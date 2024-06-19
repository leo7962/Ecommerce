using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Server.Entities;

public class User
{
    [Key] public int Id { get; set; }
    public required string Name { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public string UserName { get; set; }

    public string Password { get; set; }
    //public string Role { get; set; }
}