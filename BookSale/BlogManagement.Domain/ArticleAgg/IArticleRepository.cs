using AppFramework.Domain;
using BlogManagement.Application.Contract.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IBaseRepository<long, Article>
    {
        EditArticle GetDetails(long id);
        Article GetArticleWithCategory(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);

    }
}