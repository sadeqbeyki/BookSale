using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.Configuration.Permissions
{
    public class BlogPermissions
    {
        //Articles
        public const int ListArticles = 10;
        public const int SearchArticles = 11;
        public const int CreateArticle = 12;
        public const int EditArticle = 13;

        //ArticleCategories
        public const int ListArticleCategories = 20;
        public const int SearchArticleCategories = 21;
        public const int CreateArticleCategories = 22;
        public const int EditArticleCategories = 23;
    }
}
