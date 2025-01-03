﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kck1Sklep.Models;
using System.Threading;
using Spectre.Console;


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
            string exclamationArt = @"
 
 !!! 
!!:!!
!:::!
!:::!
!:::!
!:::!
!:::!
!:::!
!:::!
!:::!
!!:!!
 !!! 
     
 !!! 
!!:!!
 !!! 
";

            string warningText = "       OSTRZEŻENIE: MIGOTANIE EKRANU!";

            Console.Clear();
            DateTime endTime = DateTime.Now.AddSeconds(3);

            while (DateTime.Now < endTime)
            {
                Console.Clear();

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;

                // Wyśrodkowanie ostrzeżenia w konsoli
                int artX = (Console.WindowWidth - exclamationArt.Split('\n')[0].Length) / 2;
                int artY = (Console.WindowHeight - exclamationArt.Split('\n').Length) / 2;

                // Rysowanie ostrzeżenia
                foreach (var line in exclamationArt.Split('\n'))
                {
                    Console.SetCursorPosition(artX, artY++);
                    Console.WriteLine(line);
                }

                // Wyświetlenie ostrzeżenia tekstowego
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition((Console.WindowWidth - warningText.Length) / 2, artY + 1);
                Console.WriteLine(warningText);

                Thread.Sleep(100);

                Console.Clear();
                Thread.Sleep(100);
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
                // Ustawienie koloru na żółty dla napisu
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n=== Szczegóły produktu ===");
                Console.ResetColor(); // Przywrócenie domyślnego koloru

                Console.WriteLine(products[selectedIndex].GetDetails());
                Console.ResetColor();
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
