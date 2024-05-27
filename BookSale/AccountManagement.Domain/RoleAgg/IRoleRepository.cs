using AccountManagement.Application.Contracts.Role;
using AppFramework.Domain;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IBaseRepository<int, ApplicationRole>
    {
        List<RoleViewModel> GetRoles();
        EditRole GetDetails(int id);
        int GetRolesId(string roleName);
    }
}
