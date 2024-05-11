using AccountManagement.Application.Contracts.Role;
using AccountManagement.Configuration.Permissions;
using AppFramework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EndPoint.WebApp.Areas.Administration.Pages.Accounts.Role;

public class IndexModel : PageModel
{
    [TempData]
    public string Message { get; set; }
    public List<RoleViewModel> RolesList;

    private readonly IRoleApplication _roleApplication;

    public IndexModel(IRoleApplication roleApplication)
    {
        _roleApplication = roleApplication;
    }
    [NeedsPermission(AccountPermissions.ListRoles)]
    public void OnGet()
    {
        RolesList = _roleApplication.List();
    }

}
