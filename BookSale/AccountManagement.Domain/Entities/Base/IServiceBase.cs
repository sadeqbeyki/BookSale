using AccountManagement.Domain.Entities.UserAgg;

namespace AccountManagement.Domain.Entities.Base;

public interface IServiceBase
{
    Guid GetCurrentUserId();
    Task<ApplicationUser> GetCurrentUser();
    Task<IList<string>> GetCurrentUserRoles();
}
