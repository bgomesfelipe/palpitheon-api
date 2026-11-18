namespace Palpitheon.Api.Domain;

public class PoolSettings
{
    public int Id { get; set; }
    public bool VotingOpen { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}