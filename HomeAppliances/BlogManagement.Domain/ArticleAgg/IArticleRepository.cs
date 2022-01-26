using AppFramework.Domain;
using BlogManagement.Application.Contract.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public partial class Article
    {
        public interface IArticleRepository : IRepository<long, Article>
        {
            EditArticle GetDetails(long id);
            Article GetWithCategory(long id);
            List<ArticleViewModel>Search(ArticleSearchModel searchModel);

        }
    }
}
