using AppFramework.Infrastructure;

namespace AccountManagement.Configuration.Permissions
{
    public class AccountPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "User", new List<PermissionDto>
                    {
                        new PermissionDto(AccountPermissions.ListUsers, "ListUsers"),
                        new PermissionDto(AccountPermissions.SearchUser, "SearchUser"),
                        new PermissionDto(AccountPermissions.RegisterUser, "CreateUser"),
                        new PermissionDto(AccountPermissions.EditUser, "EditUser"),
                        new PermissionDto(AccountPermissions.ChangePassword, "ChangePassword"),
                    }
                },
                {
                    "Roles", new List<PermissionDto>
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
