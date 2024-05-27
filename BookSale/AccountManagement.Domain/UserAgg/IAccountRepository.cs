using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.UserAgg;
using AppFramework.Domain;

namespace AccountManagement.Domain.AccountAgg;

public interface IAccountRepository : IBaseRepository<long, ApplicationUser>
{
    ApplicationUser GetBy(string username);
    EditAccount GetDetails(long id);
    List<AccountViewModel> GetAccounts();
    List<AccountViewModel> Search(AccountSearchModel searchModel);

    void AddUserToRole(ApplicationUser user, List<string> roles);
    Task<List<string>> GetUserRolesAsync(long userId);
}
