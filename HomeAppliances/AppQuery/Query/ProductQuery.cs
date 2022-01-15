using AppQuery.Contracts.Product;
using ShopManagement.Infrastructure.EFCore;

namespace AppQuery.Query;
public class ProductQuery : IProductQuery
{
    private readonly ShopContext _context;

    public ProductQuery(ShopContext context)
    {
        _context = context;
    }

    public List<ProductQueryViewModel> GetProducts()
    {
        return _context.Products.Select(p => new ProductQueryViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            ShortDescription = p.ShortDescription,
            Description = p.Description,
            Picture = p.Picture,
            PictureAlt = p.PictureAlt,
            PictureTitle = p.PictureTitle,
            CategoryId = p.CategoryId,
            Slug = p.Slug,
            Keywords = p.Keywords,
            MetaDescription = p.MetaDescription
        }).ToList();
    }
}
