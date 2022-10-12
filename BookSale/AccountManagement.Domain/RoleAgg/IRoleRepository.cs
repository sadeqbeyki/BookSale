using AccountManagement.Application.Contracts.Role;
using AppFramework.Domain;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IBaseRepository<long, Role>
    {
        List<RoleViewModel> List();
        EditRole GetDetails(long id);
    }
}
