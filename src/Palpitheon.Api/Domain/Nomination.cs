namespace Palpitheon.Api.Domain;

public class Nomination
{
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public int NomineeId { get; set; }
    public Nominee Nominee { get; set; } = null!;
    public bool IsWinner { get; set; }
}