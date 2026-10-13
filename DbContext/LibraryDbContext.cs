namespace ConsoleApp26.DbContext;

using ConsoleApp26.Entity;
using Microsoft.EntityFrameworkCore;

public class LibraryDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Borrowing> Borrowings { get; set; }
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


        modelBuilder.Entity<Author>().HasData(
            new Author
            {
                Id = 1,
                FullName = "George Orwell",
                BirthDate = new DateTime(1965, 7, 31),
                Country = "United Kingdom"
            },
            new Author
            {
                Id = 2,
                FullName = "Harper Lee",
                BirthDate = new DateTime(1903, 6, 25),
                Country = "United States"
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Dystopian",
                Description = "Dystopian fiction is a genre of speculative fiction that explores social and political structures in a dark, nightmare world."
            },
            new Category
            {
                Id = 2,
                Name = "Classic Literature",
                Description = "Classic literature refers to works of literature that have stood the test of time and are considered exemplary or noteworthy."
            }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "1984",
                ISBN = "978-0451524935",
                PublishedYear = new DateTime(1949, 6, 8),
                Price = 9.99m,
                AuthorId = 1,
                CategoryId = 1
            },
            new Book
            {
                Id = 2,
                Title = "To Kill a Mockingbird",
                PublishedYear = new DateTime(1960, 7, 11),
                ISBN = "978-0061120084",
                Price = 7.99m,
                AuthorId = 2,
                CategoryId = 2
            }
        );

        modelBuilder.Entity<Member>().HasData(
            new Member
            {
                Id = 1,
                FullName = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "0501234567",
                RegistrationDate = new DateTime(2026, 1, 10)
            },
            new Member
            {
                Id = 2,
                FullName = "Ali Mammadov",
                Email = "ali@gmail.com",
                PhoneNumber = "0501234567",
                RegistrationDate = new DateTime(2026, 1, 10)
            }
        );

        modelBuilder.Entity<Borrowing>().HasData(
            new Borrowing
            {
                Id = 1,
                BookId = 1,
                MemberId = 1,
                BorrowDate = new DateTime(2026, 2, 1),
                DueDate = new DateTime(2026, 2, 15),
                ReturnDate = null
            },
            new Borrowing
            {
                Id = 2,
                BookId = 2,
                MemberId = 2,
                BorrowDate = new DateTime(2026, 2, 5),
                DueDate = new DateTime(2026, 2, 20),
                ReturnDate = null
            }
        );
    }
}
