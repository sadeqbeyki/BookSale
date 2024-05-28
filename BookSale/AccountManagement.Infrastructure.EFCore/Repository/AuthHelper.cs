using System.Security.Claims;
using AccountManagement.Domain.Entities.Auth;
using AccountManagement.Domain.Entities.UserAgg;
using AccountManagement.Infrastructure.EFCore.Repository.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace AccountManagement.Infrastructure.EFCore.Repository;

public class AuthHelper : ServiceBase<AuthHelper>, IAuthHelper
{
    private readonly IHttpContextAccessor _contextAccessor;
    private new readonly UserManager<ApplicationUser> _userManager;

    public AuthHelper(
        IHttpContextAccessor contextAccessor,
        UserManager<ApplicationUser> userManager,
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _contextAccessor = contextAccessor;
        _userManager = userManager;
    }

    public AuthViewModel CurrentAccountInfo()
    {
        var result = new AuthViewModel();
        if (!IsAuthenticated())
            return result;

        var claims = _contextAccessor.HttpContext.User.Claims.ToList();
        result.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId").Value);
        result.UserName = claims.FirstOrDefault(x => x.Type == "UserName").Value;
        //result.RoleId = long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
        result.FullName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        //result.Role = Roles.GetRoleBy(result.RoleId);
        return result;
    }

    public List<int> GetPermissions()
    {
        if (!IsAuthenticated())
            return new List<int>();

        var permissions = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "permissions")
            ?.Value;
        return JsonConvert.DeserializeObject<List<int>>(permissions);
    }

    public long CurrentAccountId()
    {
        return IsAuthenticated()
            ? long.Parse(_contextAccessor.HttpContext.User.Claims.First(x => x.Type == "AccountId")?.Value)
            : 0;
    }

    public string CurrentAccountMobile()
    {
        return IsAuthenticated()
            ? _contextAccessor.HttpContext.User.Claims.First(x => x.Type == "Mobile")?.Value
            : "";
    }

    public string CurrentAccountRole()
    {
        if (IsAuthenticated())
            return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
        return null;
    }

    public bool IsAuthenticated()
    {
        return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        //var claims = _contextAccessor.HttpContext.User.Claims.ToList();
        ////if (claims.Count > 0)
        ////    return true;
        ////return false;
        //return claims.Count > 0;
    }

    public async Task SignIn(AuthViewModel account)
    {
        var permissions = JsonConvert.SerializeObject(account.Permissions);

        ApplicationUser user = await _userManager.FindByIdAsync(account.Id.ToString());
        IList<string> rolesOfUser = await _userManager.GetRolesAsync(user);

        List<Claim> claims = [];
        foreach (var role in rolesOfUser)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        claims.Add(new Claim("AccountId", account.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, account.FullName));
        claims.Add(new Claim("UserName", account.UserName)); // Or Use ClaimTypes.NameIdentifier
        claims.Add(new Claim("permissions", permissions));
        claims.Add(new Claim("Mobile", account.Mobile));


        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
        };

        _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

    public void SignOut()
    {
        _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}