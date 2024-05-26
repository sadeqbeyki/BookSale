using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AppFramework.Application;
using AppFramework.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository;

public class AccountRepository : BaseRepository<long, Account>, IAccountRepository
{
    private readonly AccountContext _context;
    private readonly UserManager<Account> _userManager;



    public AccountRepository(AccountContext context, IServiceProvider serviceProvider) : base(context)
    {
        _context = context;
        _userManager = (UserManager<Account>)serviceProvider.GetService(typeof(UserManager<Account>));
        
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
            Mobile = a.PhoneNumber,
            //RoleId = a.RoleId
        }).FirstOrDefault(x => x.Id == id);
    }

    public List<AccountViewModel> Search(AccountSearchModel searchModel)
    {
        var users = _userManager.GetUsersInRoleAsync(searchModel.RoleName).Result;

            var query = users.Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                UserName = x.UserName,
                Mobile = x.PhoneNumber,
                ProfilePhoto = x.ProfilePhoto,
                //RoleId = x.RoleId,
                //Roles = x.Role.Name,
                CreationDate = x.CreationDate.ToFarsi()
            });

        if (!string.IsNullOrWhiteSpace(searchModel.UserName))
            query = query.Where(x => x.UserName.Contains(searchModel.UserName));

        if (!string.IsNullOrWhiteSpace(searchModel.FullName))
            query = query.Where(x => x.FullName.Contains(searchModel.FullName));

        if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));

        //if (searchModel.RoleId > 0)
        //    query = query.Where(x => x.RoleId == searchModel.RoleId);

        return query.OrderByDescending(x => x.Id).ToList();
    }
    public void AddUserToRole(Account user, List<string> roles)
    {
        var addUserRole = _userManager.AddToRolesAsync(user, roles);
    }

    public async Task<List<string>> GetUserRolesAsync(long userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId)
            ?? throw new Exception("User not found");
        var roles = await _userManager.GetRolesAsync(user);

        return roles.ToList();
    }
}