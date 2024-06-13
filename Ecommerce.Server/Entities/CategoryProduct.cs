namespace Ecommerce.Server.Entities
{
    public class CategoryProduct
    {
        public int IdProduct { get; set; }
        public Product Product { get; set; }
        public int IdCategory { get; set; }
        public Category Category { get; set; }
    }
}
