namespace Palpitheon.Api.Domain;

public class Nominee
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<Nomination> Nominations { get; } = [];
}