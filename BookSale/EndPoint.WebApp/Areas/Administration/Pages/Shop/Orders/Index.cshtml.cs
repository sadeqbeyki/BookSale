using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Order;

namespace EndPoint.WebApp.Areas.Administration.Pages.Shop.Orders;

public class IndexModel : PageModel
{
    public OrderSearchModel SearchModel;
    public SelectList Accounts;
    public List<OrderViewModel> Orders;

    private readonly IOrderApplication _orderApplication;
    private readonly IUserApplication _accountApplication;

    public IndexModel(IOrderApplication orderApplication, IUserApplication accountApplication)
    {
        _orderApplication = orderApplication;
        _accountApplication = accountApplication;
    }

    public void OnGet(OrderSearchModel searchModel)
    {
        Accounts = new SelectList(_accountApplication.GetAccounts(), "Id", "FullName");
        Orders = _orderApplication.Search(searchModel);
    }

    public IActionResult OnGetConfirm(long id)
    {
        _orderApplication.PaymentSucceeded(id, 0);
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetCancel(long id)
    {
        _orderApplication.Cancel(id);
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetItems(long id)
    {
        var items = _orderApplication.GetItems(id);
        return Partial("Items", items);
    }
}