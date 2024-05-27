using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EndPoint.WebApp.Pages;

public class SignInModel(IAccountApplication accountApplication) : PageModel
{
    [TempData]
    public string LoginMessage { get; set; }
    public Login Command;

    private readonly IAccountApplication _accountApplication = accountApplication;

    public void OnGet()
    {
    }
    public IActionResult OnPost(Login command)
    {
        var result = _accountApplication.Login(command).Result;
        if (result.IsSucceeded)
            return RedirectToAction("Index");

        LoginMessage = result.Message;
        return RedirectToPage("/SignIn");
    }
    public IActionResult OnGetLogOut()
    {
        _accountApplication.LogOut();
        return RedirectToPage("Index");
    }
}
