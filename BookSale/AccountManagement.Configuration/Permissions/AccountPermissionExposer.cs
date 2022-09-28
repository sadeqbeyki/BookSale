using AppFramework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Configuration.Permissions
{
    public class AccountPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "User", new List<PermissionDto>
                    {
                        new PermissionDto(AccountPermissions.ListUsers, "ListUsers"),
                        new PermissionDto(AccountPermissions.SearchUser, "SearchUsers"),
                        new PermissionDto(AccountPermissions.CreateUser, "CreateUsers"),
                        new PermissionDto(AccountPermissions.EditUser, "EditUsers"),
                    }
                },
                {
                    "UserCategory", new List<PermissionDto>
                    {
                        new PermissionDto(AccountPermissions.ListRoles, "ListRoles"),
                        new PermissionDto(AccountPermissions.SearchRole, "SearchRole"),
                        new PermissionDto(AccountPermissions.CreateRole, "CreateRole"),
                        new PermissionDto(AccountPermissions.EditRole, "EditRole"),
                    }
                }
            };
        }
    }
}
