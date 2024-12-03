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
            // Walidacja - Sprawdzamy, czy produkt jest null oraz czy ilość jest większa niż 0
            if (product == null) throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            if (quantity < 1) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than 0.");

            // Sprawdzamy, czy dostępna ilość produktu w magazynie jest wystarczająca
            if (product.Stock < quantity)
            {
                Console.WriteLine($"Niewystarczająca ilość produktu {product.Name}. Dostępne: {product.Stock} szt.");
                return;
            }

            // Sprawdzamy, czy produkt jest już w koszyku
            var existingItem = _items.FirstOrDefault(item => item.Product == product);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _items.Add(new CartItem(product, quantity));
            }

            // Zmniejszamy dostępność produktu w magazynie
            product.Stock -= quantity;
            Console.WriteLine($"{quantity} szt. {product.Name} zostało dodane do koszyka.");
        }

        public void RemoveProduct(Product product, int quantity)
        {
            // Walidacja - Sprawdzamy, czy produkt jest null oraz czy ilość jest większa niż 0
            if (product == null) throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            if (quantity < 1) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than 0.");

            // Szukamy produktu w koszyku
            var item = _items.FirstOrDefault(i => i.Product == product);
            if (item != null)
            {
                // Sprawdzamy, czy ilość do usunięcia jest mniejsza lub równa ilości w koszyku
                if (quantity >= item.Quantity)  // Usunięcie wszystkich sztuk produktu
                {
                    item.Product.Stock += item.Quantity;  // Przywracamy ilość na stanie
                    _items.Remove(item);  // Usuwamy produkt z koszyka
                    Console.WriteLine($"{item.Product.Name} zostało całkowicie usunięte z koszyka.");
                }
                else  // Usunięcie tylko wybranej ilości
                {
                    item.Quantity -= quantity;  // Zmniejszamy ilość w koszyku
                    item.Product.Stock += quantity;  // Przywracamy odpowiednią ilość na stanie
                    Console.WriteLine($"Usunięto {quantity} szt. {product.Name} z koszyka.");
                }
            }
            else
            {
                // Jeśli produkt nie znajduje się w koszyku, rzucamy wyjątek
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
