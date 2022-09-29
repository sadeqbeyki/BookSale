using AppFramework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.Configuration.Permissions
{
    public class BlogPermissionExposer
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
                        new PermissionDto(BlogPermissions.CreateArticle, "CreateArticles"),
                        new PermissionDto(BlogPermissions.EditArticle, "EditArticles"),
                    }
                },
                {
                    "ArticleCategory", new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.ListArticles, "ListArticles"),
                        new PermissionDto(BlogPermissions.SearchArticles, "SearchArticles"),
                        new PermissionDto(BlogPermissions.CreateArticle, "CreateArticle"),
                        new PermissionDto(BlogPermissions.EditArticle, "EditArticle"),
                    }
                }
            };
        }
    }
}
