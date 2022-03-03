using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account;
public class IndexModel : PageModel
{
    [TempData] 
    public string Message { get; set; }
    public AccountSearchModel SearchModel;
    public List<AccountViewModel> Accounts;
    public SelectList Roles;

    private readonly IAccountApplication _accountApplication;

    public IndexModel(IAccountApplication productApplication)
    {
        _accountApplication = productApplication;
    }

    public void OnGet(AccountSearchModel searchModel)
    {
        Accounts = _accountApplication.Search(searchModel);
    }
    public PartialViewResult OnGetCreate()
    {
        var command = new CreateAccount 
        { 
        };
        return Partial("./Create", command);
    }

    public JsonResult OnPostCreate(CreateAccount command)
    {
        var result = _accountApplication.Create(command);
        return new JsonResult(result);
    }

    public PartialViewResult OnGetEdit(long id)
    {
        var account = _accountApplication.GetDetails(id);
        return Partial("Edit", account);
    }
    public JsonResult OnPostEdit(EditAccount command)
    {
        var result = _accountApplication.Edit(command);
        return new JsonResult(result);
    }
}
