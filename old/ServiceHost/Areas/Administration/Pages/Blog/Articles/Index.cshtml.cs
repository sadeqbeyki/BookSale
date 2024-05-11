using AppFramework.Infrastructure;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles;

public class IndexModel : PageModel
{
    public ArticleSearchModel SearchModel;
    public List<ArticleViewModel> Articles;
    public SelectList ArticleCategories;

    private readonly IArticleCategoryApplication _articleCategoryApplication;
    private readonly IArticleApplication _articleApplication;

    public IndexModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
    {
        _articleApplication = articleApplication;
        _articleCategoryApplication = articleCategoryApplication;
    }
    [NeedsPermission(BlogPermissions.ListArticles)]
    public void OnGet(ArticleSearchModel searchModel)
    {
        ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(),"Id","Name");
        Articles = _articleApplication.Search(searchModel);
    }
}
