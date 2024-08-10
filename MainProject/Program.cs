var builder = WebApplication.CreateBuilder(args);

// START    Configuration of the web application goes in here
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
