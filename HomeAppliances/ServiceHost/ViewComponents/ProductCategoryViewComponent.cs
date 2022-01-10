using AppQuery.Contracts.ProductCategory;
using AppQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoriesQuery;

        public ProductCategoryViewComponent(IProductCategoryQuery productCategoriesQuery)
        {
            _productCategoriesQuery = productCategoriesQuery;
        }

        public IViewComponentResult Invoke()
        {
            var productCategories = _productCategoriesQuery.GetProductCategories();
            return View(productCategories);
        }
    }
}
