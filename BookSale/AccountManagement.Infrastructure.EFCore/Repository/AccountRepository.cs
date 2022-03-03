using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AppFramework.Application;
using AppFramework.Infrastructure;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _accountContext;

        public AccountRepository(AccountContext context) : base(context)
        {
            _accountContext = context;
        }
        public EditAccount GetDetails(long id)
        {
            return _accountContext.Accounts.Select(a => new EditAccount
            {
                Id = a.Id,
                FullName = a.FullName,
                UserName = a.UserName,
                Mobile = a.Mobile,
                RoleId = a.RoleId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _accountContext.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                UserName = x.UserName,
                Mobile = x.Mobile,
                ProfilePhoto = x.ProfilePhoto,
                RoleId = 2,
                Role = "مدیر سیستم",
                CreationDate = x.CreationDate.ToFarsi()
            });
            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
                query = query.Where(x => x.FullName.Contains(searchModel.FullName));

            if (!string.IsNullOrWhiteSpace(searchModel.UserName))
                query = query.Where(x => x.UserName.Contains(searchModel.UserName));

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));

            if (searchModel.RoleId > 0)
                query = query.Where(x => x.RoleId == searchModel.RoleId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}