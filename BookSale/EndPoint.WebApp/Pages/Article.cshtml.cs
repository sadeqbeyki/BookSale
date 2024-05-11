using AppQuery.Contracts.Article;
using AppQuery.Contracts.ArticleCategory;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EndPoint.WebApp.Pages;

public class ArticleModel : PageModel
{
    public ArticleQueryModel Article;
    public List<ArticleCategoryQueryModel> ArticleCategories;
    public List<ArticleQueryModel> LatestArticles;

    private readonly IArticleQuery _articleQuery;
    private readonly IArticleCategoryQuery _articleCategoryQuery;
    private readonly ICommentApplication _commentApplication;


    public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery, ICommentApplication commentApplication)
    {
        _articleQuery = articleQuery;
        _articleCategoryQuery = articleCategoryQuery;
        _commentApplication = commentApplication;
    }

    public void OnGet(string id)
    {
        Article = _articleQuery.GetArticleDetails(id);
        LatestArticles = _articleQuery.LatestArticles();
        ArticleCategories = _articleCategoryQuery.GetArticleCategories();
    }
    public IActionResult OnPost(AddComment command, string articleSlug)
    {
        command.Type = CommentType.Article;
        _commentApplication.Add(command);
        return RedirectToPage("/Article", new { Id = articleSlug });
    }
}
