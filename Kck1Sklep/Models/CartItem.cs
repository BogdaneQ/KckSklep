using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kck1Sklep.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public CartItem(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            if (quantity < 1) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than 0.");
            Quantity = quantity;
        }
        public decimal GetTotalPrice()
        {
            return Product.Price * Quantity;
        }

        public override string ToString()
        {
            return $"{Product.Name} (Ilość: {Quantity}) - {GetTotalPrice():C}";
        }
    }

}
