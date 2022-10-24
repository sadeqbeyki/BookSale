using AppFramework.Infrastructure;

namespace CommentManagement.Infrastructure.Configuration.Permissions
{
    public class CommentPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Comment", new List<PermissionDto>
                    {
                        new PermissionDto(CommentPermissions.ListComments,"ListComments"),
                        new PermissionDto(CommentPermissions.SearchComments,"SearchComment"),
                        new PermissionDto(CommentPermissions.AddComment,"AddComment"),
                        new PermissionDto(CommentPermissions.ConfirmComment,"ConfirmComment"),
                        new PermissionDto(CommentPermissions.CancelComment,"CancelComment")
                    }
                }
            };
        }
    }
}
