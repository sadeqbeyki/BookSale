using AccountManagement.Application.Contracts.Account;
using AppFramework.Domain;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IBaseRepository<long, Account>
    {
        Account GetBy(string username);
        EditAccount GetDetails(long id);
        List<AccountViewModel> GetAccounts();
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}
