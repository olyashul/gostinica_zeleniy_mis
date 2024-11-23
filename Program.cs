using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ptoba_svoego_vhoda_reg_2.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ptoba_svoego_vhoda_reg_2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ptoba_svoego_vhoda_reg_2Context") ?? throw new InvalidOperationException("Connection string 'ptoba_svoego_vhoda_reg_2Context' not found.")));


builder.Logging.ClearProviders(); // Очищаем существующих провайдеров логов для большей гибкости
builder.Logging.AddConsole(options =>
{
    options.IncludeScopes = true; // Включаем scopes для лучшей трассировки
    options.LogToStandardErrorThreshold = LogLevel.Error; // Логи уровня Error и выше в stderr
});
builder.Logging.AddDebug(); // Логи уровня Debug и выше в Debug-консоль (Visual Studio)
builder.Logging.SetMinimumLevel(LogLevel.Information); // Устанавливаем минимальный уровень логирования - Information


// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHttpContextAccessor(); // Добавьте эту строку
builder.Services.AddSession(options => {
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});




var app = builder.Build();


app.UseRequestLocalization(options =>
{
    var supportedCultures = new[]
    {
           new CultureInfo("ru-RU"), // Русский
           
       };

    options.DefaultRequestCulture = new RequestCulture("ru-RU");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession(); // Это очень важно!  Должно быть ДО app.UseRouting()


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "brons",
    pattern: "brons/{action=Index}/{id?}",
    defaults: new { controller = "Brons" });

app.MapControllerRoute(
    name: "users",
    pattern: "users/{action=Index}/{id?}",
    defaults: new { controller = "Users" });


app.Run();
