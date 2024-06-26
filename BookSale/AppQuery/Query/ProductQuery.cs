﻿using AppFramework.Application;
using AppQuery.Contracts.Comment;
using AppQuery.Contracts.Product;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore;

namespace AppQuery.Query;
public class ProductQuery : IProductQuery
{
    private readonly ShopContext _shopContext;
    private readonly InventoryContext _inventoryContext;
    private readonly DiscountContext _discountContext;
    private readonly CommentContext _commentContext;
    public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext, CommentContext commentContext)
    {
        _shopContext = shopContext;
        _inventoryContext = inventoryContext;
        _discountContext = discountContext;
        _commentContext = commentContext;
    }

    public List<ProductQueryModel> GetLatestArrivals()
    {
        var inventory = _inventoryContext.Inventory
            .Select(x => new { x.ProductId, x.UnitPrice }).ToList();
        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new { x.DiscountRate, x.ProductId }).ToList();

        var products = _shopContext.Products
            .Include(pc => pc.Category)
            .Select(p => new ProductQueryModel
            {
                Id = p.Id,
                Category = p.Category.Name,
                Name = p.Name,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Slug = p.Slug
            }).AsNoTracking().ToList();

        foreach (var product in products)
        {
            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);

            if (productInventory != null)
            {
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }
        }
        return products;
    }

    public ProductQueryModel GetProductDetails(string slug)
    {
        var inventory = _inventoryContext.Inventory
            .Select(x => new { x.ProductId, x.UnitPrice, x.InStock }).ToList();
        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

        var product = _shopContext.Products
            .Include(p => p.Category)
            .Include(p => p.ProductPictures)
            .Select(p => new ProductQueryModel
            {
                Id = p.Id,
                Category = p.Category.Name,
                Name = p.Name,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Slug = p.Slug,
                CategorySlug = p.Category.Slug,
                Code = p.Code,
                Description = p.Description,
                Keywords = p.Keywords,
                MetaDescription = p.MetaDescription,
                ShortDescription = p.ShortDescription,
                Pictures = MapProductPictures(p.ProductPictures)
            }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

        if (product == null)
            return new ProductQueryModel();

        var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
        if (productInventory != null)
        {
            product.IsInStock = productInventory.InStock;
            var price = productInventory.UnitPrice;
            product.Price = price.ToMoney();
            product.DoublePrice = price;
            var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
            if (discount != null)
            {
                var discountRate = discount.DiscountRate;
                product.DiscountRate = discountRate;
                product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                product.HasDiscount = discountRate > 0;
                var discountAmount = Math.Round((price * discountRate) / 100);
                product.PriceWithDiscount = (price - discountAmount).ToMoney();
            }
        }

        product.Comments = _commentContext.Comments
            .Where(x => !x.IsCanceled)
            .Where(x => x.IsConfirmed)
            .Where(x => x.Type == CommentType.Product)
            .Where(x => x.OwnerRecordId == product.Id)
            .Select(x => new CommentQueryModel
            {
                Id = x.Id,
                Message = x.Message,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).ToList();

        return product;
    }

    private static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> productPictures)
    {
        return productPictures.Select(x => new ProductPictureQueryModel
        {
            ProductId = x.ProductId,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            IsRemoved = x.IsRemoved
        }).Where(x => !x.IsRemoved).ToList();
    }

    public List<ProductQueryModel> Search(string value)
    {
        var inventory = _inventoryContext.Inventory
            .Select(x => new { x.ProductId, x.UnitPrice }).ToList();
        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();
        var query = _shopContext.Products
            .Include(x => x.Category)
            .Select(p => new ProductQueryModel
            {
                Id = p.Id,
                Category = p.Category.Name,
                CategorySlug = p.Category.Slug,
                Name = p.Name,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                ShortDescription = p.ShortDescription,
                Slug = p.Slug
            }).AsNoTracking();

        if (!string.IsNullOrWhiteSpace(value))
            query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));
        var products = query.OrderByDescending(x => x.Id).ToList();

        foreach (var product in products)
        {
            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null)
            {
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount == null) continue;

                var discountRate = discount.DiscountRate;
                product.DiscountRate = discountRate;
                product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                product.HasDiscount = discountRate > 0;
                var discountAmount = Math.Round((price * discountRate) / 100);
                product.PriceWithDiscount = (price - discountAmount).ToMoney();
            }
        }
        return products;
    }

    public List<CartItem> CheckInventoryStatus(List<CartItem> cartItems)
    {
        var inventory = _inventoryContext.Inventory.ToList();

        foreach (var cartItem in cartItems.Where(cartItem =>
            inventory.Any(x => x.ProductId == cartItem.Id && x.InStock)))
        {
            var itemInventory = inventory.Find(x => x.ProductId == cartItem.Id);
            cartItem.IsInStock = itemInventory.CalculateCurrentCount() >= cartItem.Count;
        }

        return cartItems;
    }
}
