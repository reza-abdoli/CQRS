using System.Reflection;
using Hanssens.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using UI.Contracts;
using UI.Services;
using UI.Services.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();


builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login";
    });

// builder.Services.AddHttpClient<IClient, Client>(client =>
// {
//     client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiAddress").Value);
// });

var apiAddress = builder.Configuration.GetValue<string>("ApiAddress");

builder.Services.AddHttpClient<IClient, Client>(client =>
{
    client.BaseAddress = new Uri(apiAddress);
})
.AddTypedClient<IClient>((httpClient, sp) => new Client(apiAddress, httpClient));

builder.Services.AddSingleton<ILoacalStorageService, LoacalStorageService>();
builder.Services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ILoacalStorageService, LoacalStorageService>();

builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();

builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCookiePolicy();
app.UseAuthentication();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();