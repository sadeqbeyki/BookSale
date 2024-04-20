using AppFramework.Infrastructure;
using DiscountManagement.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Configuration.Permissions;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Configuration;

public class DiscountConfigureServices
{
    public static void Configure(IServiceCollection services,string connectionString)
    {
        services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
        services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

        services.AddTransient<IColleagueDiscountApplication, ColleagueDiscountApplication>();
        services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();

        services.AddScoped<IPermissionExposer, DiscountPermissionExposer>();

        services.AddDbContext<DiscountContext>(x => x.UseSqlServer(connectionString));
    }
}