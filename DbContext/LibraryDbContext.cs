namespace ConsoleApp26.DbContext;

using ConsoleApp26.Entity;
using Microsoft.EntityFrameworkCore;

public class LibraryDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseSqlServer("Server=IBO\\SQLEXPRESS;Database=LibraryDb;trusted_connection=true;integrated security=true;TrustServerCertificate=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>().HasKey(a => a.Id);
        modelBuilder.Entity<Book>().HasKey(b => b.Id);
        modelBuilder.Entity<Borrowing>().HasKey(b => b.Id);
        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        modelBuilder.Entity<Member>().HasKey(m => m.Id);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Borrowing>()
            .HasOne(b => b.Book)
            .WithMany(m => m.Borrowings)
            .HasForeignKey(b => b.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Borrowing>()
            .HasOne(b => b.Member)
            .WithMany(m => m.Borrowings)
            .HasForeignKey(b => b.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Member>()
            .HasIndex(m => m.Email)
            .IsUnique();

        modelBuilder.Entity<Book>()
            .HasIndex(b => b.ISBN)
            .IsUnique();

        modelBuilder.Entity<Book>()
            .Property(b => b.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Author>()
            .Property(a => a.FullName)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Author>()
            .Property(a => a.Country)
            .IsRequired()
            .HasMaxLength(60);

        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<Book>()
            .Property(b => b.ISBN)
            .IsRequired()
            .HasMaxLength(20);

        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Category>()
            .Property(c => c.Description)
            .HasMaxLength(300);

        modelBuilder.Entity<Member>()
            .Property(m => m.FullName)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Member>()
            .Property(m => m.Email)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<Member>()
            .Property(m => m.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        modelBuilder.Entity<Author>().ToTable("Authors");
        modelBuilder.Entity<Book>().ToTable("Books");
        modelBuilder.Entity<Borrowing>().ToTable("Borrowings");
        modelBuilder.Entity<Category>().ToTable("Categories");
        modelBuilder.Entity<Member>().ToTable("Members");

        modelBuilder.Entity<Borrowing>()
            .Property(b => b.BorrowDate)
            .IsRequired();

        modelBuilder.Entity<Borrowing>()
            .Property(b => b.DueDate)
            .IsRequired();

        modelBuilder.Entity<Borrowing>()
            .Property(b => b.ReturnDate)
            .IsRequired(false);
    }
}
