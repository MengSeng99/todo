using Microsoft.EntityFrameworkCore;
using TodoApi.Core.Entities;

namespace TodoApi.Infrastructure.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TodoItem>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<TodoItem>()
                .Property(t => t.Description)
                .HasMaxLength(500);
        }
    }
} 