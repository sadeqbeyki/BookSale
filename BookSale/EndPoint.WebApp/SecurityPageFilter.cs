using AccountManagement.Domain.Auth;
using AppFramework.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace EndPoint.WebApp;

public class SecurityPageFilter : IPageFilter
{
    private readonly IAuthHelper _authHelper;

    public SecurityPageFilter(IAuthHelper authHelper)
    {
        _authHelper = authHelper;
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        var handlerPermission =
            (NeedsPermissionAttribute)context.HandlerMethod.MethodInfo.GetCustomAttribute(
                typeof(NeedsPermissionAttribute));

        if (handlerPermission == null)
            return;

        var accountPermissions = _authHelper.GetPermissions();

        if (accountPermissions.All(x => x != handlerPermission.Permission))
            context.HttpContext.Response.Redirect("/Auth");
    }

    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
    }
}
