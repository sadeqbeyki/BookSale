using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AppFramework.Application;
using AppFramework.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : BaseRepository<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName
            }).ToList();
        }

        public Account GetBy(string username)
        {
            return _context.Accounts.FirstOrDefault(x => x.UserName == username);
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts.Select(a => new EditAccount
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
            var query = _context.Accounts.Include(x => x.Role)
                .Select(x => new AccountViewModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    UserName = x.UserName,
                    Mobile = x.Mobile,
                    ProfilePhoto = x.ProfilePhoto,
                    RoleId = x.RoleId,
                    Role = x.Role.Name,
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