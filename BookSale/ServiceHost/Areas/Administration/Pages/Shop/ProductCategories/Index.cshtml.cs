using AppFramework.Application;
using AppFramework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories;
//[Authorize(Roles = "1, 3")]
public class IndexModel : PageModel
{
    public ProductCategorySearchModel SearchModel;
    public List<ProductCategoryViewModel> ProductCategories;
    private readonly IProductCategoryApplication _productCategoryApplication;

    public IndexModel(IProductCategoryApplication productCategoryApplication)
    {
        _productCategoryApplication = productCategoryApplication;
    }
    [NeedsPermission(ShopPermissions.ListProductCategories)]
    public void OnGet(ProductCategorySearchModel searchModel)
    {
        ProductCategories = _productCategoryApplication.Search(searchModel);
    }
    public PartialViewResult OnGetCreate()
    {
        return Partial("./Create", new CreateProductCategory());
    }
    [NeedsPermission(ShopPermissions.CreateProductCategories)]
    public JsonResult OnPostCreate(CreateProductCategory command)
    {
        var result = _productCategoryApplication.Create(command);
        return new JsonResult(result);
    }

    public PartialViewResult OnGetEdit(long id)
    {
        var productCategory = _productCategoryApplication.GetDetails(id);
        return Partial("Edit", productCategory);
    }
    [NeedsPermission(ShopPermissions.EditProductCategories)]
    public IActionResult OnPostEdit(EditProductCategory command)
    {
        if (!ModelState.IsValid)
        {
            return new JsonResult(new { message = "فایل انتخاب شده مجاز نیست" });
        }

        var result = _productCategoryApplication.Edit(command);
        return new JsonResult(result);
    }
}
