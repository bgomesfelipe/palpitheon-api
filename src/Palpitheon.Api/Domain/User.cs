using Microsoft.AspNetCore.Identity;

namespace Palpitheon.Api.Domain;

public class User : IdentityUser<Guid>
{
    public ICollection<UserPick> Picks { get; } = [];
}