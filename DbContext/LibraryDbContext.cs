namespace ConsoleApp26.DbContext;
using Microsoft.EntityFrameworkCore;

public class LibraryDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseSqlServer("Server=TITAN006\\SQLEXPRESS;Database=LibraryDb;trusted_connection=true;integrated security=true;TrustServerCertificate=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
