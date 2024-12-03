using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kck1Sklep.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public Product(int id, string name, decimal price, int stock, string description, string category)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Product name cannot be null or empty.", nameof(name));
            if (price < 0) throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");

            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
            Category = category;
        }

        public string GetBasicInfo()
        {
            return $"{Name} - {Price:C}";
        }

        public string GetDetails()
        {
            return $"ID: {Id} | {Name} - {Price:C}\nNa stanie: {Stock}\nOpis: {Description}";
        }
    }
}
