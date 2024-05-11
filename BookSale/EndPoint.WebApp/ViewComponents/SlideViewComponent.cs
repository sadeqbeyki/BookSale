using AppQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.WebApp.ViewComponents;

public class SlideViewComponent : ViewComponent
{
    private readonly ISideQuery _slideQuery;

    public SlideViewComponent(ISideQuery slideQuery)
    {
        _slideQuery = slideQuery;
    }

    public IViewComponentResult Invoke()
    {
        var slides = _slideQuery.GetSlides();
        return View(slides);
    }
}
