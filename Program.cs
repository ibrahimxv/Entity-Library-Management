using ConsoleApp26.DbContext;
using Microsoft.EntityFrameworkCore;
using var dbContext = new LibraryDbContext();

var books = await dbContext.Books
    .Include(b => b.Author)
    .Include(b => b.Category)
    .ToListAsync();

foreach (var book in books)
{
    Console.WriteLine($"Book: {book.Title}, Author: {book.Author.FullName}, Category: {book.Category.Name}");
}

var member = await dbContext.Members
    .Include(m => m.Borrowings)
    .ThenInclude(b => b.Book)
    .FirstOrDefaultAsync(m => m.Id == 1);

if (member != null)
{
    Console.WriteLine($"Member: {member.FullName}");
    foreach (var borrowing in member.Borrowings)
    {
        Console.WriteLine($" - Book: {borrowing.Book.Title}, Borrow Date: {borrowing.BorrowDate}, Due Date: {borrowing.DueDate}");
    }
}