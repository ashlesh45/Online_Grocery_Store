using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Grocery_Store
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } // ✅ Changed Name to ProductName
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public CartItem(int productId, string productName, int quantity, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }
    }




}
