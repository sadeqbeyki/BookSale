using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Configuration.Permissions;
using AppFramework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account;
[Authorize(Roles = Roles.Administrator)]
public class IndexModel : PageModel
{
    [TempData]
    public string Message { get; set; }
    public AccountSearchModel SearchModel;
    public List<AccountViewModel> Accounts;
    public SelectList RolesSelectList;

    private readonly IRoleApplication _roleApplication;
    private readonly IAccountApplication _accountApplication;

    public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
    {
        _accountApplication = accountApplication;
        _roleApplication = roleApplication;
    }
    [NeedsPermission(AccountPermissions.ListUsers)]
    public void OnGet(AccountSearchModel searchModel)
    {
        RolesSelectList = new SelectList(_roleApplication.List(), "Id", "Name");
        Accounts = _accountApplication.Search(searchModel);
    }
    public PartialViewResult OnGetCreate()
    {
        var command = new RegisterAccount
        {
            Roles = _roleApplication.List()
        };
        return Partial("./Create", command);
    }
    [NeedsPermission(AccountPermissions.RegisterUser)]
    public JsonResult OnPostCreate(RegisterAccount command)
    {
        var result = _accountApplication.Register(command);
        return new JsonResult(result);
    }

    public PartialViewResult OnGetEdit(long id)
    {
        var account = _accountApplication.GetDetails(id);
        account.Roles = _roleApplication.List();
        return Partial("Edit", account);
    }
    [NeedsPermission(AccountPermissions.EditUser)]
    public JsonResult OnPostEdit(EditAccount command)
    {
        var result = _accountApplication.Edit(command);
        return new JsonResult(result);
    }
    public PartialViewResult OnGetChangePassword(long id)
    {
        var command = new ChangePassword { Id = id };
        return Partial("ChangePassword", command);
    }
    [NeedsPermission(AccountPermissions.ChangePassword)]
    public JsonResult OnPostChangePassword(ChangePassword command)
    {
        var result = _accountApplication.ChangePassword(command);
        return new JsonResult(result);
    }
}
