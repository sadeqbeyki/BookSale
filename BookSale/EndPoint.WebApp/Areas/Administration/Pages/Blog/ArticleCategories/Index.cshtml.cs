using AppFramework.Infrastructure;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EndPoint.WebApp.Areas.Administration.Pages.Blog.ArticleCategories;

public class IndexModel : PageModel
{
    public ArticleCategorySearchModel SearchModel;
    public List<ArticleCategoryViewModel> ArticleCategories;
    private readonly IArticleCategoryApplication _articleCategoryApplication;

    public IndexModel(IArticleCategoryApplication articleCategoryApplication)
    {
        _articleCategoryApplication = articleCategoryApplication;
    }
    [NeedsPermission(BlogPermissions.ListArticleCategories)]
    public void OnGet(ArticleCategorySearchModel searchModel)
    {
        ArticleCategories = _articleCategoryApplication.Search(searchModel);
    }
    public PartialViewResult OnGetCreate()
    {
        return Partial("./Create", new CreateArticleCategory());
    }
    [NeedsPermission(BlogPermissions.CreateArticleCategory)]
    public JsonResult OnPostCreate(CreateArticleCategory command)
    {
        var result = _articleCategoryApplication.Create(command);
        return new JsonResult(result);
    }

    public PartialViewResult OnGetEdit(long id)
    {
        var articleCategory = _articleCategoryApplication.GetDetails(id);
        return Partial("Edit", articleCategory);
    }
    [NeedsPermission(BlogPermissions.EditArticleCategory)]
    public IActionResult OnPostEdit(EditArticleCategory command)
    {
        var result = _articleCategoryApplication.Edit(command);
        return new JsonResult(result);
    }
}
