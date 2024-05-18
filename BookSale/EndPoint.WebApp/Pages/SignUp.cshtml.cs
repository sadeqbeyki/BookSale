using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EndPoint.WebApp.Pages;

public class SignUpModel : PageModel
{
    [TempData]
    public string RegisterMessage { get; set; }
    private readonly IAccountApplication _accountApplication;

    public SignUpModel(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }
    public void OnGet()
    {
    }
    public IActionResult OnPostRegister(RegisterAccount command)
    {
        var result = _accountApplication.Register(command);
        if (result.IsSucceeded)
            return RedirectToPage("/SignIn");
        RegisterMessage = result.Message;
        return RedirectToPage("/SignUp");
    }
}
