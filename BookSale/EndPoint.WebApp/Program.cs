using AccountManagement.Configuration;
using AppFramework.Application;
using AppFramework.Application.Sms;
using AppFramework.Application.ZarinPal;
using AppFramework.Infrastructure;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Configuration;
using EndPoint.WebApp;
using InventoryManagement.Configuration;
using InventoryManagement.Presentation.Api;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using ShopManagement.Configuration;
using ShopManagement.Presentation.Api;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
var connectionString = builder.Configuration.GetConnectionString("BookSaleDB");
ShopConfigureServices.Configure(builder.Services, connectionString);
DiscountConfigureServices.Configure(builder.Services, connectionString);
InventoryConfigureServices.Configure(builder.Services, connectionString);
CommentConfigureServices.Configure(builder.Services, connectionString);
BlogConfigureServices.Configure(builder.Services, connectionString);
AccountConfigureServices.Configure(builder.Services, connectionString);

builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
builder.Services.AddTransient<ISmsService, SmsService>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = new PathString("/Auth");
        options.LogoutPath = new PathString("/Auth");
        options.AccessDeniedPath = new PathString("/AccessDenied");
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminArea", builder => builder.RequireRole(new List<string> { Roles.Administrator, Roles.ContentUploader }))
    .AddPolicy("Comment", builder => builder.RequireRole(new List<string> { Roles.Administrator, Roles.ContentUploader }))
    .AddPolicy("Shop", builder => builder.RequireRole(new List<string> { Roles.Administrator }))
    .AddPolicy("Discount", builder => builder.RequireRole(new List<string> { Roles.Administrator }))
    .AddPolicy("Account", builder => builder.RequireRole(new List<string> { Roles.Administrator }));

builder.Services.AddRazorPages()
    .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Comments", "Comment");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");

    })
    .AddApplicationPart(typeof(ProductController).Assembly)
    .AddApplicationPart(typeof(InventoryController).Assembly)
    .AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
