namespace Palpitheon.Api.Domain;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int DisplayOrder { get; set; }
    public ICollection<Nomination> Nominations { get; } = [];
}