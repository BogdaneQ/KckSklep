using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kck1Sklep.Models;
using System.Threading;

namespace Kck1Sklep.Views
{
    public class View
    {
        private string sklepLogo = @"

          ::::::::       :::::::::       ::::::::       :::::::::   :::::::::::    :::   ::: 
        :+:    :+:      :+:    :+:     :+:    :+:      :+:    :+:      :+:        :+:   :+:  
       +:+             +:+    +:+     +:+    +:+      +:+    +:+      +:+         +:+ +:+    
      +#++:++#++      +#++:++#+      +#+    +:+      +#++:++#:       +#+          +#++:      
            +#+      +#+            +#+    +#+      +#+    +#+      +#+           +#+        
    #+#    #+#      #+#            #+#    #+#      #+#    #+#      #+#           #+#         
    ########       ###             ########       ###    ###      ###           ###          

";

        public void ShowEpilepsyWarning()
        {
            string triangleWithSkull = @"
                 ▲                   
                ▲ ▲                   
               ▲   ▲                  
              ▲     ▲                
             ▲       ▲               
            ▲         ▲              
           ▲           ▲             
          ▲             ▲            
         ▲               ▲           
        ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲                ";

            string warningText = "       OSTRZEŻENIE: MIGOTANIE EKRANU!";

            Console.Clear();
            DateTime endTime = DateTime.Now.AddSeconds(3);

            while (DateTime.Now < endTime)
            {
                Console.Clear();

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;

                int triangleX = (Console.WindowWidth - 33) / 2;
                int triangleY = Console.WindowHeight / 2 - 6;

                Console.SetCursorPosition(triangleX, triangleY);
                Console.WriteLine("                   ▲                   ");
                Console.SetCursorPosition(triangleX, triangleY + 1);
                Console.WriteLine("                  ▲ ▲                  ");
                Console.SetCursorPosition(triangleX, triangleY + 2);
                Console.WriteLine("                 ▲   ▲                 ");
                Console.SetCursorPosition(triangleX, triangleY + 3);
                Console.WriteLine("                ▲     ▲                ");
                Console.SetCursorPosition(triangleX, triangleY + 4);
                Console.WriteLine("               ▲       ▲               ");
                Console.SetCursorPosition(triangleX, triangleY + 5);
                Console.WriteLine("              ▲         ▲              ");
                Console.SetCursorPosition(triangleX, triangleY + 6);
                Console.WriteLine("             ▲           ▲             ");
                Console.SetCursorPosition(triangleX, triangleY + 7);
                Console.WriteLine("            ▲             ▲            ");
                Console.SetCursorPosition(triangleX, triangleY + 8);
                Console.WriteLine("           ▲               ▲           ");
                Console.SetCursorPosition(triangleX, triangleY + 9);
                Console.WriteLine("          ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲          ");
                Console.SetCursorPosition(triangleX, triangleY + 10);
                Console.ForegroundColor = ConsoleColor.Red;

                Console.SetCursorPosition((Console.WindowWidth - warningText.Length) / 2, triangleY + 12);
                Console.WriteLine(warningText);

                Thread.Sleep(200);

                Console.Clear();
                Thread.Sleep(200);
            }

            Console.ResetColor();
            Console.Clear();
        }

        public void ShowMainMenu(string[] menuOptions, int selectedIndex)
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(sklepLogo);
            Console.WriteLine("\n=== Main Menu ===");

            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"  {menuOptions[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {menuOptions[i]}");
                }
            }
        }

        public string GetSearchQuery()
        {
            Console.Write("Wpisz nazwę produktu, którego szukasz: ");
            return Console.ReadLine();
        }

        public (decimal, decimal) GetPriceRange()
        {
            Console.Write("Podaj minimalną cenę: ");
            decimal minPrice = GetDecimalInput();

            Console.Write("Podaj maksymalną cenę: ");
            decimal maxPrice = GetDecimalInput();

            return (minPrice, maxPrice);
        }

        public int GetMinStock()
        {
            Console.Write("Podaj minimalną dostępność (ilość na stanie): ");
            return GetIntInput();
        }

        public void ShowCategories(string[] categories, int selectedIndex)
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("\n=== Kategorie produktów ===");
            for (int i = 0; i < categories.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"  {categories[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {categories[i]}");
                }
            }
        }

        public void ShowProducts(List<Product> products, int selectedIndex)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("\n=== Lista produktów ===");

            for (int i = 0; i < products.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"  {products[i].GetBasicInfo()}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {products[i].GetBasicInfo()}");
                }
            }

            if (products.Any())
            {
                Console.WriteLine("\n=== Szczegóły produktu ===");
                Console.WriteLine(products[selectedIndex].GetDetails());
            }
        }

        public void ShowCartItems(List<CartItem> cartItems, int selectedIndex)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("\n=== Koszyk ===");

            if (cartItems.Count == 0)
            {
                Console.WriteLine("Koszyk jest pusty.");
            }
            else
            {
                for (int i = 0; i < cartItems.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"  {cartItems[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {cartItems[i]}");
                    }
                }
            }
        }

        public int GetProductQuantityInput(string action = "dodaj")
        {
            Console.Write($"Ile sztuk chcesz {action}?: ");
            return GetIntInput();
        }

        public string GetAddressInput()
        {
            Console.Write("\nPodaj adres dostawy: ");
            return Console.ReadLine();
        }

        public int GetIntInput()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Podaj prawidłową liczbę całkowitą.");
                }
            }
        }

        private decimal GetDecimalInput()
        {
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Podaj prawidłową wartość.");
                }
            }
        }
    }
}
