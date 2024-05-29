using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.Entities.Auth;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.AccountAcl;

public class ShopAccountAcl : IShopAccountAcl
{
    private readonly IUserApplication _accountApplication;
    private readonly IAuthHelper _authHelper;


    public ShopAccountAcl(IUserApplication accountApplication, IAuthHelper authHelper)
    {
        _accountApplication = accountApplication;
        _authHelper = authHelper;
    }

    public (string name, string mobile) GetAccountBy(long id)
    {
        var account = _accountApplication.GetAccountBy(id);
        return (account.FullName, account.Mobile);
    }
    public long CurrentAccountId()
    {
        return _authHelper.CurrentAccountId();
    }
}