using AppFramework.Infrastructure;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nest;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class EditModel : PageModel
    {
        //public SelectList ArticleCategories;
        public EditArticle Command;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IArticleApplication _articleApplication;
        public EditModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }

        //public void OnGet(long id)
        //{
        //    Command = _articleApplication.GetDetails(id);
        //    ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Title");
        //}

        public IActionResult OnGet(int id)
        {
            Command = _articleApplication.GetDetails(id);
            Command.Categories = _articleCategoryApplication.GetArticleCategories().ToList();
            
            return Page();
        }
        [NeedsPermission(BlogPermissions.EditArticle)]
        public IActionResult OnPost(int id, EditArticle command)
        {
            _articleApplication.Edit(command);
            return RedirectToPage("./Index");
        }
    }
}
