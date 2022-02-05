namespace AppQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel GetArticleCategory(string slug);
        List<ArticleCategoryQueryModel> GetArticleCategories();
    }
}
