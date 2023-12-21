using PaymentOrderWeb.MVC.Configurations;
using PaymentOrderWeb.Application.Configurations;
using PaymentOrderWeb.Domain.Configurations;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddWebModule();
builder.Services.AddAppModule();
builder.Services.AddDomainModule();

builder.Services.AddControllers()
                         .AddJsonOptions(o => o.JsonSerializerOptions
                                .ReferenceHandler = ReferenceHandler.Preserve);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
