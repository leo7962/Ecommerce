namespace Ecommerce.Server.Dtos;

public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> IdOrders { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }
}