
using Microsoft.EntityFrameworkCore;

namespace WebApi.DBOperations
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) // kurucu fonksiyon 
        {
        }

        public DbSet<Book> Books { get; set; } // Books tablosu ile Book class'ını ilişkilendirdik

    }
}