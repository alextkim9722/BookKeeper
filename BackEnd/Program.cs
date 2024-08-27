using Microsoft.EntityFrameworkCore;
using BackEnd.Services;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using BackEnd.Model;
using BackEnd.Services.Context;
using BackEnd.Services.Generics;
using BackEnd.Services.Generics.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// START    Configuration of the web application goes in here
builder.Services.AddDbContext<BookShelfContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<Identification, IdentityRole>().AddEntityFrameworkStores<BookShelfContext>().AddDefaultTokenProviders();
builder.Services.AddCors(x => x.AddPolicy("mainpolicy", builder => { builder.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader(); }));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IReviewService, ReviewService>();
builder.Services.AddTransient<IIdentificationService, IdentificationService>();
builder.Services.AddTransient<IRoleService, RoleService>();

builder.Services.AddTransient<IGenericService<Book>, GenericService<Book>>();
builder.Services.AddTransient<IGenericService<User>, GenericService<User>>();
builder.Services.AddTransient<IGenericService<Genre>, GenericService<Genre>>();
builder.Services.AddTransient<IGenericService<Author>, GenericService<Author>>();
builder.Services.AddTransient<IGenericService<Review>, GenericService<Review>>();

builder.Services.AddTransient<IJunctionService<User_Book>, JunctionService<User_Book>>();
builder.Services.AddTransient<IJunctionService<Book_Author>, JunctionService<Book_Author>>();
builder.Services.AddTransient<IJunctionService<Book_Genre>, JunctionService<Book_Genre>>();
builder.Services.AddTransient<IJunctionService<Review>, JunctionService<Review>>();

var app = builder.Build(); // END

app.UseCors("mainpolicy");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
