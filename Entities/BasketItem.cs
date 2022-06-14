namespace API.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        // navigation properties to create a relationship between a basket item and a product 1 to 1 relationship. NEXT 2 LINES
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}