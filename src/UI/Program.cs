using System.Reflection;
using Hanssens.Net;
using UI.Contracts;
using UI.Services;
using UI.Services.Base;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<IClient, Client>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiAddress").Value);
});
builder.Services.AddSingleton<ILoacalStorageService, LoacalStorageService>();
builder.Services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ILoacalStorageService,LoacalStorageService>();

builder.Services.AddScoped<ILeaveTypeService,LeaveTypeService>();

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

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();