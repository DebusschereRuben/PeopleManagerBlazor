using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PeopleManager.Model
{
    [Table(nameof(Organization))]
    public class Organization
    {
        public int Id { get; set; }

        public required string OwnerUserId { get; set; }
        public IdentityUser OwnerUser { get; set; } = null!;

        public required string Name { get; set; }

        public string? Description { get; set; }

        public IList<Person> Members { get; set; } = new List<Person>();

    }
}
