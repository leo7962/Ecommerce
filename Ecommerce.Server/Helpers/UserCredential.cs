using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Ecommerce.Server.Helpers
{
    public class UserCredential
    {
        [Required][EmailAddress] public string Email { get; set; }
        [Required][PasswordPropertyText] public string Password { get; set; }
    }
}
