using AppFramework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repositories
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }
        public EditProduct GetDetails(long id)
        {
            return _context.Products.Select(p => new EditProduct
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                Slug = p.Slug,
                CategoryId = p.CategoryId,
                Description = p.Description,
                Keywords = p.Keywords,
                MetaDescription = p.MetaDescription,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                ShortDescription = p.ShortDescription,
                UnitPrice = p.UnitPrice
            }).FirstOrDefault(p => p.Id == id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _context.Products
                .Include(x => x.Category)
                .Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category.Name,
                CategoryId = x.CategoryId,
                Code = x.Code,
                Picture = x.Picture,
                UnitPrice = x.UnitPrice,
                InStock = x.InStock,
                CreationDate = x.CreationDate.ToString()
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));

            if (searchModel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
