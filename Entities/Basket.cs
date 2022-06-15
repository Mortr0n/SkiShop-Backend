using System.Collections.Generic;
using System.Linq;

namespace API.Entities
{
    public class Basket
    {
        
        public int Id { get; set; }
        public string BuyerId { get; set; }

        // Initializing a new list of basket items to prevent any list undefined or null scenarios since a
        // new list is initialized every time a basket is created
        // public List<BasketItem> Items {get; set;} = new List<BasketItem>();
        // Don't have to specify the item your creating if you do it on the same line so you can shortcut to new();
        public List<BasketItem> Items { get; set; } = new();

        public void AddItem(Product product, int quantity)
        {
            if (Items.All(item => item.ProductId != product.Id))
            {
                Items.Add(new BasketItem{Product = product, Quantity = quantity});
            }

            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);
            if (existingItem != null) existingItem.Quantity += quantity;
        }
        
        public void RemoveItem(int productId, int quantity)
        {
            var item = Items.FirstOrDefault(item => item.ProductId == productId);
            if (item == null) return;
            item.Quantity -= quantity;
            if (item.Quantity <= 0) Items.Remove(item);
        }
    }
}