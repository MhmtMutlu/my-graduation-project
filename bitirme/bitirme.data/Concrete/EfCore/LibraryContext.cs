using Microsoft.EntityFrameworkCore;
using bitirme.entity;

namespace bitirme.data.Concrete.EfCore
{
    public class LibraryContext:DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=nnyDb");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategory>()
                .HasKey(c => new {c.CategoryId, c.BookId});
        }
        
    }
}