using AppFramework.Infrastructure;

namespace BlogManagement.Infrastructure.Configuration.Permissions
{
    public class BlogPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Article", new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.ListArticles, "ListArticles"),
                        new PermissionDto(BlogPermissions.SearchArticles, "SearchArticles"),
                        new PermissionDto(BlogPermissions.CreateArticle, "CreateArticle"),
                        new PermissionDto(BlogPermissions.EditArticle, "EditArticle"),
                    }
                },
                {
                    "ArticleCategory", new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.ListArticleCategories, "ListArticleCategories"),
                        new PermissionDto(BlogPermissions.SearchArticleCategories, "SearchArticleCategories"),
                        new PermissionDto(BlogPermissions.CreateArticleCategory, "CreateArticleCategory"),
                        new PermissionDto(BlogPermissions.EditArticleCategory, "EditArticleCategory"),
                    }
                }
            };
        }
    }
}
