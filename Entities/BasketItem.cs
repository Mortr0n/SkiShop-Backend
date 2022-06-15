using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    // Data Anotation to change the name to plural for the database.
    [Table("BasketItems")]
    public class BasketItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        // navigation properties to create a relationship between a basket item and a product 1 to 1 relationship. NEXT 2 LINES
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}