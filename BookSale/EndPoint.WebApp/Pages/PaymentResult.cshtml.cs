using AppFramework.Application.ZarinPal;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EndPoint.WebApp.Pages;

public class PaymentResultModel : PageModel
{
    public PaymentResult Result;

    public void OnGet(PaymentResult result)
    {
        Result = result;
    }
}