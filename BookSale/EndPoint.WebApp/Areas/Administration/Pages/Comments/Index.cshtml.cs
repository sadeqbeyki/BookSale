using AppFramework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EndPoint.WebApp.Areas.Administration.Pages.Comments;

public class IndexModel : PageModel
{
    [TempData]
    public string Message { get; set; }
    public List<CommentViewModel> Comments;
    public CommentSearchModel SearchModel;
    private readonly ICommentApplication _commentApplication;

    public IndexModel(ICommentApplication commentApplication)
    {
        _commentApplication = commentApplication;
    }
    [NeedsPermission(CommentPermissions.ListComments)]
    public void OnGet(CommentSearchModel searchModel)
    {
        Comments = _commentApplication.Search(searchModel);
    }
    [NeedsPermission(CommentPermissions.CancelComment)]
    public IActionResult OnGetCancel(long id)
    {
        var result = _commentApplication.Cancel(id);
        if (result.IsSucceeded)
            return RedirectToPage("./Index");

        Message = result.Message;
        return RedirectToPage("./Index");
    }
    [NeedsPermission(CommentPermissions.ConfirmComment)]
    public IActionResult OnGetConfirm(long id)
    {
        var result = _commentApplication.Confirm(id);
        if (result.IsSucceeded)
            return RedirectToPage("./Index");

        Message = result.Message;
        return RedirectToPage("./Index");
    }
}
