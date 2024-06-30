using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Server.Helpers;

public class UserCredential
{
    [Required][EmailAddress] public string Email { get; set; }
    [Required] public string UserName { get; set; }
    [Required][PasswordPropertyText] public string Password { get; set; }
}