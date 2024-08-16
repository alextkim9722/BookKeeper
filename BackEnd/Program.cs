using Microsoft.EntityFrameworkCore;
using BackEnd.Services;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using BackEnd.Model;
using BackEnd.Model;

var builder = WebApplication.CreateBuilder(args);

// START    Configuration of the web application goes in here
builder.Services.AddDbContext<BookShelfContext>(options =>
    options.UseSqlServer(
        builder
        .Configuration
        .GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<Identification, IdentityRole>().
    AddEntityFrameworkStores<BookShelfContext>().
    AddDefaultTokenProviders();

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IGenreService, GenreService>();

var app = builder.Build(); // END

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
