using AppFramework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discounts.ColleagueDiscounts;
//[Authorize(Roles = Roles.Administrator)]

public class IndexModel : PageModel
{
    [TempData]
    public string Message { get; set; }
    public ColleagueDiscountSearchModel SearchModel;
    public List<ColleagueDiscountViewModel> ColleagueDiscounts;
    public SelectList Products;

    private readonly IProductApplication _productApplication;
    private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
    public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication colleagueDiscountApplication)
    {
        _productApplication = productApplication;
        _colleagueDiscountApplication = colleagueDiscountApplication;
    }
    [NeedsPermission(DiscountPermissions.ListColleagueDiscounts)]
    public void OnGet(ColleagueDiscountSearchModel searchModel)
    {
        Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        ColleagueDiscounts = _colleagueDiscountApplication.Search(searchModel);
    }
    public PartialViewResult OnGetCreate()
    {
        var command = new DefineColleagueDiscount
        {
            Products = _productApplication.GetProducts()
        };
        return Partial("./Create",command);
    }
    [NeedsPermission(DiscountPermissions.DefineColleagueDiscount)]
    public JsonResult OnPostCreate(DefineColleagueDiscount command)
    {
        var result = _colleagueDiscountApplication.Define(command);
        return new JsonResult(result);
    }
    public PartialViewResult OnGetEdit(long id)
    {
        var colleagueDiscount = _colleagueDiscountApplication.GetDetails(id);
        colleagueDiscount.Products = _productApplication.GetProducts();
        return Partial("Edit", colleagueDiscount);
    }
    [NeedsPermission(DiscountPermissions.EditColleagueDiscount)]

    public JsonResult OnPostEdit(EditColleagueDiscount command)
    {
        var result = _colleagueDiscountApplication.Edit(command);
        return new JsonResult(result);
    }
    [NeedsPermission(DiscountPermissions.RemoveColleagueDiscount)]
    public IActionResult OnGetRemove(long id)
    {
        _colleagueDiscountApplication.Remove(id);
        return RedirectToPage("./Index");
    }
    [NeedsPermission(DiscountPermissions.RestoreColleagueDiscount)]
    public IActionResult OnGetRestore(long id)
    {
        _colleagueDiscountApplication.Restore(id);
        return RedirectToPage("./Index");
    }
}
