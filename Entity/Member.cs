namespace ConsoleApp26.Entity;

public class Member
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime RegistrationDate { get; set; }

    public ICollection<Borrowing> Borrowings { get; set; }
}
