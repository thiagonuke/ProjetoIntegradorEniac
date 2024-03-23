using Desafio_Processo.UI.Controllers;
using Desafio_Processo.UI.RequestAPI;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc().AddControllersAsServices();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<Requests>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
