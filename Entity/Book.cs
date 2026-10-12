namespace ConsoleApp26.Entity;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public DateTime PublishedYear { get; set; } 
    public decimal Price { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<Borrowing> Borrowings { get; set; }
}
