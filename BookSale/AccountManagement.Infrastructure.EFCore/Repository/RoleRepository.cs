using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AppFramework.Application;
using AppFramework.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : BaseRepository<int, Role>, IRoleRepository
    {
        private readonly AccountContext _accountContext;
        private readonly RoleManager<Role> _roleManager;

        public RoleRepository(AccountContext accountContext, IServiceProvider serviceProvider) : base(accountContext)
        {
            _accountContext = accountContext;
            _roleManager = (RoleManager<Role>)serviceProvider.GetService(typeof(RoleManager<Role>));
        }

        public EditRole GetDetails(int id)
        {
            var role = _accountContext.Roles.Select(x => new EditRole
            {
                Id = x.Id,
                Name = x.Name,
                MappedPermissions = MapPermissions(x.Permissions)
            }).AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

            role.Permissions = role.MappedPermissions.Select(x => x.Code).ToList();

            return role;
        }

        private static List<PermissionDto> MapPermissions(IEnumerable<Permission> permissions)
        {
            return permissions.Select(x => new PermissionDto(x.Code, x.Name)).ToList();
        }

        public List<RoleViewModel> GetRoles()
        {
            return _accountContext.Roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name,
                CreationDate = r.CreationDate.ToFarsi()
            }).ToList();
        }

        public int GetRolesId(string roleName)
        {
            var role = _accountContext.Roles
                .FirstOrDefault(r => r.Name == roleName);
            return role.Id;
        }

        public string GetRoleName(Role role)
        {
            var roleName = _roleManager.GetRoleNameAsync(role).Result;
            return roleName;
        }
    }
}
