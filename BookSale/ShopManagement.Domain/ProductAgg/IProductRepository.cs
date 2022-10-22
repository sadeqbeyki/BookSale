using AppFramework.Domain;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IBaseRepository<long, Product>
    {
        EditProduct? GetDetails(long id);
        Product GetProductWithCategory(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts();
    }
}
