using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repositories;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstraper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            //IServiceCollection services = new ServiceCollection();
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
           
        }

    }
}