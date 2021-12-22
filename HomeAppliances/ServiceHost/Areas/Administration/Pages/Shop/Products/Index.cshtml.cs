using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ProductSearchModel SearchModel;
        public List<ProductViewModel> Products;
        public SelectList ProductCategories;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductSearchModel searchModel)
        {
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name"); 
            Products = _productApplication.Search(searchModel);
        }
        public PartialViewResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                //Categories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name")
                Categories = _productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }

        public PartialViewResult OnGetEdit(long id)
        {
            var product = _productApplication.GetDetails(id);
            //product.Categories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id" , "Name");
            product.Categories = _productCategoryApplication.GetProductCategories();
            return Partial("Edit", product);
        }
        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _productApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetOutOfStock(long id)
        {
            var result = _productApplication.OutOfStock(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message=result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetIsInStock(long id)
        {
            var result = _productApplication.IsInStock(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
