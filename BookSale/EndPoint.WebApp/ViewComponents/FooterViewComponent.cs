using Microsoft.AspNetCore.Mvc;

namespace EndPoint.WebApp.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
