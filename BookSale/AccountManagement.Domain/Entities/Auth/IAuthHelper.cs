namespace AccountManagement.Domain.Entities.Auth;

public interface IAuthHelper
{
    void SignOut();
    bool IsAuthenticated();
    Task SignIn(AuthViewModel account);
    string CurrentAccountRole();
    AuthViewModel CurrentAccountInfo();
    List<int> GetPermissions();
    long CurrentAccountId();
    string CurrentAccountMobile();
}
