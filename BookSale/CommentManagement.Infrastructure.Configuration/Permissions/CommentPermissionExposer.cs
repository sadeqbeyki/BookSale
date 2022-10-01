using AppFramework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentManagement.Infrastructure.Configuration.Permissions
{
    public class CommentPermissionExposer
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


                    }
                }
            };
        }
    }
}
