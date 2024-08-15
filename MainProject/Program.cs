using Microsoft.EntityFrameworkCore;

using MainProject.Services;
using MainProject.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// START    Configuration of the web application goes in here
builder.Services.AddDbContext<BookShelfContext>(options =>
    options.UseSqlServer(
        builder
        .Configuration
        .GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IShelfPageService, ShelfPageService>();
// END

var app = builder.Build(); // Builds the web application

app.UseStaticFiles(); // Allows for the use of static files

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MainPage}/{action=ShelfPage}/{id?}"
    );

app.Run(); // Starts the program
