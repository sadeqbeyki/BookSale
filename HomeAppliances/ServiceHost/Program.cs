using AppFramework.Application;
using DiscountManagement.Configuration;
using InventoryManagement.Configuration;
using Microsoft.EntityFrameworkCore;
using ServiceHost;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);

//add services to the container.
var connectionString = builder.Configuration.GetConnectionString("HomeAppliancesDB");
ShopManagementBootstraper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);

builder.Services.AddTransient<IFileUploader, FileUploader>();
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
