using EquipmentCheckout.Persistence.Ef;
using Microsoft.EntityFrameworkCore;
using EquipmentCheckout.Ui.Queries;
using EquipmentCheckout.Persistence.Queries;
using EquipmentCheckout.Domain.Services;
using EquipmentCheckout.Domain.Daos;
using EquipmentCheckout.Persistence.Daos;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// -----------------------------------------------------------------------------
// WHY IS THIS HERE?
//
// By default, ASP.NET Core MVC expects views to live in a top-level folder:
//     /Views/{Controller}/{Action}.cshtml
//
// However, in this assignment we are organizing the project by ARCHITECTURE LAYER.
// That means all UI-related files (controllers, views, viewmodels, read models)
// live under the /Ui folder instead of using the default MVC layout:
//
//     /Ui/Controllers
//     /Ui/Views
//     /Ui/ViewModels
//
// Because of this, we must tell MVC where to look for view files.
// Otherwise it will try to load them from /Views/... and fail with
// "The view 'X' was not found".
//
// The code below changes MVC's search paths so that when a controller returns:
//
//     return View();
//
// MVC will look here instead:
//
//     /Ui/Views/{Controller}/{Action}.cshtml
//     /Ui/Views/Shared/{View}.cshtml
//
// Example:
//     EquipmentController -> Index()
//     MVC will load:
//         /Ui/Views/Equipment/Index.cshtml
//
// {1} = Controller name (without "Controller")
// {0} = Action name
//
// This is purely a configuration change so our folder structure can match the
// architectural layering taught in this course. Nothing else about MVC changes.
// -----------------------------------------------------------------------------
builder.Services.Configure<Microsoft.AspNetCore.Mvc.Razor.RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Clear();
    options.ViewLocationFormats.Add("/Ui/Views/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("/Ui/Views/Shared/{0}.cshtml");
});


// SQLite database (AppData folder is copied to output on build).
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "AppData", "equipment-checkout.db");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite($"Data Source={dbPath}");
});

// TODO: register Domain services, DAOs, and read-model gateways here.
// Example pattern:
builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddScoped<IEquipmentDao, EquipmentDao>();
builder.Services.AddScoped<IBorrowerDao, BorrowerDao>();
builder.Services.AddScoped<ILoanDao, LoanDao>();
builder.Services.AddScoped<IEquipmentReadModelGateway, EquipmentReadModelGateway>();
builder.Services.AddScoped<IBorrowerReadModelGateway, BorrowerReadModelGateway>();
builder.Services.AddScoped<ILoanReadModelGateway, LoanReadModelGateway>();



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

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
