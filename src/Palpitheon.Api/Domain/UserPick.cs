namespace Palpitheon.Api.Domain;

public class UserPick
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public int CategoryId { get; set; }
    public int NomineeId { get; set; }
    public Nomination Nomination { get; set; } = null!;
    public DateTimeOffset UpdatedAt { get; set; }
}