using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Server.Entities;

public class User
{
    [Key] public int IdUser { get; set; }
    [Required] public string Name { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    [Required] public string UserName { get; set; }
    [Required][EmailAddress]public string Email { get; set; }
    [Required][PasswordPropertyText] public string Password { get; set; }
    [Required] public string Role { get; set; }
}