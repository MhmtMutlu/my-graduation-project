using bitirme.entity;
using Microsoft.EntityFrameworkCore;

namespace bitirme.data.Concrete.EfCore
{
    public class DepartmentContext:DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Article> Articles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=nnyDb");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentLesson>()
                .HasKey(i => new {i.DepartmentId, i.LessonId});
            
            modelBuilder.Entity<LessonNote>()
                .HasKey(i => new {i.LessonId, i.NoteId});

            modelBuilder.Entity<DepartmentArticle>()
                .HasKey(i => new {i.DepartmentId, i.ArticleId});
        }
    }
}