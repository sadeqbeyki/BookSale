using AppQuery.Contracts.ProductCategory;
using AppQuery.Contracts.Slide;
using AppQuery.Query;
using DiscountManagement.Configuration;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Configuration;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repositories;

var builder = WebApplication.CreateBuilder(args);

//add services to the container.
var connectionString = builder.Configuration.GetConnectionString("HomeAppliancesDB");
ShopManagementBootstraper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configure(builder.Services, connectionString);

//var connectionString = builder.Configuration.GetConnectionString("HomeAppliancesDB");
//builder.Services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));

//builder.Services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
//builder.Services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

//builder.Services.AddTransient<IProductRepository, ProductRepository>();
//builder.Services.AddTransient<IProductApplication, ProductApplication>();

//builder.Services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
//builder.Services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

//builder.Services.AddTransient<ISlideApplication, SlideApplication>();
//builder.Services.AddTransient<ISlideRepository, SlideRepository>();

//builder.Services.AddTransient<ISideQuery, SlideQuery>();
//builder.Services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
