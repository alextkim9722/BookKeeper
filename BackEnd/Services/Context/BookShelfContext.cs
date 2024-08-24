using Microsoft.EntityFrameworkCore;
using BackEnd.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BackEnd.Services.Context
{
    public class BookShelfContext : IdentityDbContext<Identification>
    {
        public BookShelfContext(DbContextOptions<BookShelfContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Book_Genre> Book_Genre { get; set; }
        public DbSet<Book_Author> Book_Author { get; set; }
        public DbSet<User_Book> User_Book { get; set; }
    }
}
