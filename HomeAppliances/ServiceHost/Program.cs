using Microsoft.EntityFrameworkCore;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repositories;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Application.Contracts.ProductPicture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("HomeAppliancesDB");
//ShopManagementBootstraper.Configure(services, connectionString);

var connectionString = builder.Configuration.GetConnectionString("HomeAppliancesDB");
builder.Services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
builder.Services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>(); 
builder.Services.AddTransient<IProductApplication, ProductApplication>();
builder.Services.AddTransient<IProductPictureRepository,ProductPictureRepository>();
builder.Services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

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
