using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kck1Sklep.Models
{
    public class Cart
    {
        private List<CartItem> _items = new List<CartItem>();

        public List<CartItem> GetItems()
        {
            return _items;
        }
        public decimal GetTotal()
        {
            return _items.Sum(item => item.GetTotalPrice());
        }


        public void AddProduct(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            if (quantity < 1) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than 0.");

            if (product.Stock < quantity)
            {
                Console.WriteLine($"Niewystarczająca ilość produktu {product.Name}. Dostępne: {product.Stock} szt.");
                return;
            }

            var existingItem = _items.FirstOrDefault(item => item.Product == product);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _items.Add(new CartItem(product, quantity));
            }

            product.Stock -= quantity;
            Console.WriteLine($"{quantity} szt. {product.Name} zostało dodane do koszyka.");
        }

        public void RemoveProduct(Product product, int quantity)
        {

            if (product == null) throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            if (quantity < 1) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than 0.");

            var item = _items.FirstOrDefault(i => i.Product == product);
            if (item != null)
            {
                if (quantity >= item.Quantity)
                {
                    item.Product.Stock += item.Quantity;
                    _items.Remove(item);
                    Console.WriteLine($"{item.Product.Name} zostało całkowicie usunięte z koszyka.");
                }
                else
                {
                    item.Quantity -= quantity;
                    item.Product.Stock += quantity;
                    Console.WriteLine($"Usunięto {quantity} szt. {product.Name} z koszyka.");
                }
            }
            else
            {
                throw new InvalidOperationException("Product not found in cart.");
            }
        }

        public void ShowCart()
        {
            if (_items.Count == 0)
            {
                Console.WriteLine("Koszyk jest pusty.");
            }
            else
            {
                Console.WriteLine("Produkty w koszyku:");
                foreach (var item in _items)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public void ClearCart()
        {
            _items.Clear();
        }

    }
}
