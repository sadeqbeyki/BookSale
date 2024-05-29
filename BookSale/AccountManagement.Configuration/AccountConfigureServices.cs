using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Configuration.Permissions;
using AccountManagement.Domain.Entities.RoleAgg;
using AccountManagement.Domain.Entities.UserAgg;
using AccountManagement.Infrastructure.EFCore;
using AccountManagement.Infrastructure.EFCore.Repository;
using AppFramework.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Configuration;

public class AccountConfigureServices
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<IUserApplication, UserApplication>();
        services.AddScoped<IUserRepository,UserRepository>();

        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IRoleApplication,RoleApplication>();

        services.AddScoped<IPermissionExposer, AccountPermissionExposer>();

        services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlServer(connectionString));
    }
}