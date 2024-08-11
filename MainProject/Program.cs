using Microsoft.EntityFrameworkCore;

using MainProject.Datastore;

var builder = WebApplication.CreateBuilder(args);

// START    Configuration of the web application goes in here
builder.Services.AddDbContext<BookShelfContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Server=DESKTOP-550OG8P\\MSSQLSERVER2022;Database=BookKeeperDB_Test;Trusted_Connection=True;")));
builder.Services.AddControllersWithViews();
// END

var app = builder.Build(); // Builds the web application

app.UseStaticFiles(); // Allows for the use of static files

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Shelf}/{id?}"
    );

app.Run(); // Starts the program
