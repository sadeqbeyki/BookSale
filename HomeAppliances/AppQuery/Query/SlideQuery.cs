using AppQuery.Contracts.Slide;
using ShopManagement.Infrastructure.EFCore;

namespace AppQuery.Query
{
    public class SlideQuery:ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetSlide()
        {
            return _context.Slides.Where(x=>x.IsRemoved==false).Select(x=>new SlideQueryModel
            {
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Heading=x.Heading,
                Title=x.Title,
                Text=x.Text,
                Link=x.Link,
                BtnText=x.BtnText
            }).ToList();
        }
    }
}
