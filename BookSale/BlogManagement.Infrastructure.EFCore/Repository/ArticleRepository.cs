using AppFramework.Application;
using AppFramework.Infrastructure;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using static BlogManagement.Domain.ArticleAgg.Article;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly BlogContext _blogContext;

        public ArticleRepository(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public EditArticle GetDetails(long id)
        {
            return _blogContext.Articles.Select(x => new EditArticle
            {
                Id = x.Id,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                CanonicalAddress = x.CanonicalAddress
            }).FirstOrDefault(x => x.Id == id);
        }

        public Article GetWithCategory(long id)
        {
            return _blogContext.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _blogContext.Articles.Select(x => new ArticleViewModel
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Title = x.Title,
                Picture = x.Picture,
                ShortDescription = x.ShortDescription.Substring(0, Math.Min(x.ShortDescription.Length, 50)) + "...",
                PublishDate = x.PublishDate.ToFarsi(),
                Category = x.Category.Name
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title));
            if (searchModel.CategoryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
