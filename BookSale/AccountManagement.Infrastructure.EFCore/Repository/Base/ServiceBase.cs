using AccountManagement.Domain.Const;
using AccountManagement.Domain.Entities.Base;
using AccountManagement.Domain.Entities.UserAgg;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AccountManagement.Infrastructure.EFCore.Repository.Base;

public class ServiceBase<TService> : IServiceBase where TService : class
{
    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly UserManager<ApplicationUser> _userManager;

    protected ServiceBase(IServiceProvider serviceProvider)
    {
        _httpContextAccessor = (IHttpContextAccessor)serviceProvider.GetService(typeof(IHttpContextAccessor));
        _userManager = (UserManager<ApplicationUser>)serviceProvider.GetService(typeof(UserManager<ApplicationUser>));
    }


    public Guid GetCurrentUserId()
    {
        ClaimsIdentity identity = _httpContextAccessor?.HttpContext?.User?.Identity as ClaimsIdentity;
        Guid userId = Guid.Parse(identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        return userId;

        //    return _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    //return _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }


    public async Task<ApplicationUser> GetCurrentUser()
    {
        // Obtain MailId from token
        ClaimsIdentity identity = _httpContextAccessor?.HttpContext?.User?.Identity as ClaimsIdentity;
        var userMailId = identity?.FindFirst(AppConstants.JWT_SUB)?.Value;

        // Obtain user from token
        ApplicationUser user = null;
        if (!string.IsNullOrEmpty(userMailId))
        {
            user = await _userManager.FindByEmailAsync(userMailId);
        }

        return user;
    }

    public async Task<IList<string>> GetCurrentUserRoles()
    {
        ApplicationUser currentUser = await GetCurrentUser();
        IList<string> userRoles = await _userManager.GetRolesAsync(currentUser);

        if (userRoles.Count > 0)
        {
            return userRoles;
        }
        throw new Exception("not found");
    }



}
