using AppFramework.Domain;
using CommentManagement.Application.Contracts.Comment;

namespace CommentManagement.Domain.CommentAgg;

public interface ICommentRepository : IBaseRepository<long, Comment>
{
    List<CommentViewModel> Search(CommentSearchModel searchModel);
}
