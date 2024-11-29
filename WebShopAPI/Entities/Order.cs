namespace WebShopAPI.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigációs tulajdonságok
        public virtual Customer Customer { get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
