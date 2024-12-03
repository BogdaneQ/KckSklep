using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kck1Sklep.Models;
using Kck1Sklep.Views;

namespace Kck1Sklep.Controllers
{
    public class Controller
    {
        private List<Product> _products;
        private Cart _cart;
        private View _view;
        private string[] _menuOptions =
        {
        "Przeglądaj kategorie produktów",
        "Pokaż koszyk",
        "Finalizuj zakupy",
        "Wyszukaj produkt po nazwie",
        "Filtruj produkty po cenie",
        "Filtruj produkty po dostępności",
        "Wyjdź"
    };
        private string[] _categories = { "Piłki", "Odzież", "Obuwie", "Akcesoria" };

        public Controller()
        {
            _view = new View();
            _cart = new Cart();
            _products = new List<Product>
    {
        // Piłki
        new Product(1, "Piłka nożna", 100.99m, 20, "Piłka nożna do gry na trawie.", "Piłki"),
        new Product(2, "Piłka do koszykówki", 120.00m, 10, "Piłka do koszykówki o wysokiej przyczepności.", "Piłki"),
        new Product(3, "Piłka do siatkówki", 80.00m, 12, "Lekka piłka do gry w siatkówkę.", "Piłki"),
        new Product(4, "Piłka do ręcznej", 110.50m, 8, "Piłka do gry w piłkę ręczną, rozmiar dla dorosłych.", "Piłki"),
        new Product(5, "Piłka do tenisa", 10.99m, 50, "Zestaw 3 piłek do tenisa ziemnego.", "Piłki"),

        // Obuwie
        new Product(6, "Buty sportowe", 199.99m, 15, "Wygodne buty sportowe.", "Obuwie"),
        new Product(7, "Buty do biegania", 249.99m, 18, "Buty do biegania z doskonałą amortyzacją.", "Obuwie"),
        new Product(8, "Buty halowe", 179.99m, 10, "Buty halowe idealne do sportów zespołowych.", "Obuwie"),
        new Product(9, "Buty do koszykówki", 299.99m, 5, "Specjalistyczne buty do koszykówki.", "Obuwie"),
        new Product(10, "Buty trekkingowe", 350.00m, 7, "Wodoodporne buty trekkingowe na długie wędrówki.", "Obuwie"),

        // Odzież
        new Product(11, "Koszulka sportowa", 89.99m, 25, "Oddychająca koszulka sportowa.", "Odzież"),
        new Product(12, "Spodenki treningowe", 59.99m, 30, "Lekkie spodenki treningowe.", "Odzież"),
        new Product(13, "Bluza z kapturem", 149.99m, 12, "Sportowa bluza z kapturem.", "Odzież"),
        new Product(14, "Legginsy sportowe", 99.99m, 20, "Elastyczne legginsy do biegania.", "Odzież"),
        new Product(15, "Kurtka przeciwdeszczowa", 199.99m, 8, "Lekka, wodoodporna kurtka sportowa.", "Odzież"),

        // Akcesoria
        new Product(16, "Rękawice bramkarskie", 129.99m, 5, "Rękawice dla bramkarzy.", "Akcesoria"),
        new Product(17, "Czapka z daszkiem", 49.99m, 40, "Czapka sportowa chroniąca przed słońcem.", "Akcesoria"),
        new Product(18, "Skarpetki sportowe", 29.99m, 50, "Zestaw 3 par skarpetek sportowych.", "Akcesoria"),
        new Product(19, "Plecak sportowy", 139.99m, 10, "Pojemny plecak na sprzęt sportowy.", "Akcesoria"),
        new Product(20, "Bidon na wodę", 19.99m, 100, "Bidon na wodę o pojemności 750 ml.", "Akcesoria")
    };
        }


        public void Run()
        {
            int selectedIndex = 0;
            bool running = true;
            Stack<int> menuStack = new Stack<int>();  // Stos do śledzenia poprzednich menu

            while (running)
            {
                _view.ShowMainMenu(_menuOptions, selectedIndex);
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = selectedIndex == 0 ? _menuOptions.Length - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = selectedIndex == _menuOptions.Length - 1 ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        menuStack.Push(selectedIndex); // Zapisujemy obecne menu na stos
                        HandleMenuSelection(selectedIndex, menuStack);
                        break;
                    case ConsoleKey.Backspace:
                        if (menuStack.Count > 0)
                        {
                            selectedIndex = menuStack.Pop();  // Powrót do poprzedniego menu
                        }
                        break;
                }
            }
        }

        private void HandleMenuSelection(int selectedIndex, Stack<int> menuStack)
        {
            switch (selectedIndex)
            {
                case 0:
                    BrowseCategories(menuStack);
                    break;
                case 1:
                    ShowCart(menuStack);
                    break;
                case 2:
                    FinalizeOrder();
                    break;
                case 3:
                    SearchProductByName(menuStack);
                    break;
                case 4:
                    FilterProductsByPrice(menuStack);
                    break;
                case 5:
                    FilterProductsByStock(menuStack);
                    break;
                case 6:
                    Console.WriteLine("Dziękujemy za zakupy!");
                    Environment.Exit(0);
                    break;
            }
        }

        // Wyszukiwanie produktu po nazwie
        private void SearchProductByName(Stack<int> menuStack)
        {
            string query = _view.GetSearchQuery();
            var results = _products.Where(p => p.Name.ToLower().Contains(query.ToLower())).ToList();
            if (results.Count > 0)
            {
                ShowProductList(results, menuStack);  // Przekazanie menuStack do metody ShowProductList
            }
            else
            {
                Console.WriteLine("Nie znaleziono produktów o podanej nazwie.");
                Console.ReadKey();
            }
        }


        // Filtrowanie produktów po cenie
        private void FilterProductsByPrice(Stack<int> menuStack)
        {
            var (minPrice, maxPrice) = _view.GetPriceRange();
            var filteredProducts = _products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
            if (filteredProducts.Count > 0)
            {
                ShowProductList(filteredProducts, menuStack);
            }
            else
            {
                Console.WriteLine("Nie znaleziono produktów w podanym zakresie cen.");
                Console.ReadKey();
            }
        }

        // Filtrowanie produktów po dostępności
        private void FilterProductsByStock(Stack<int> menuStack)
        {
            int minStock = _view.GetMinStock();
            var filteredProducts = _products.Where(p => p.Stock >= minStock).ToList();
            if (filteredProducts.Count > 0)
            {
                ShowProductList(filteredProducts, menuStack);
            }
            else
            {
                Console.WriteLine("Nie znaleziono produktów z taką dostępnością.");
                Console.ReadKey();
            }
        }

        private void BrowseCategories(Stack<int> menuStack)
        {
            int selectedIndex = 0;
            bool browsing = true;

            while (browsing)
            {
                _view.ShowCategories(_categories, selectedIndex);
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = selectedIndex == 0 ? _categories.Length - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = selectedIndex == _categories.Length - 1 ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        var filteredProducts = _products.Where(p => p.Category == _categories[selectedIndex]).ToList();
                        ShowProductList(filteredProducts, menuStack);
                        break;
                    case ConsoleKey.Backspace:
                        browsing = false;
                        break;
                }
            }
        }

        private void ShowProductList(List<Product> products, Stack<int> menuStack)
        {
            int selectedIndex = 0;
            bool selecting = true;

            while (selecting)
            {
                _view.ShowProducts(products, selectedIndex);
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = selectedIndex == 0 ? products.Count - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = selectedIndex == products.Count - 1 ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        int quantity = _view.GetProductQuantityInput();

                        // Attempt to add product to the cart
                        _cart.AddProduct(products[selectedIndex], quantity);

                        // Optionally, you can ask the user if they want to continue adding items
                        Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować dodawanie lub Esc, aby wrócić do listy produktów.");
                        if (Console.ReadKey().Key == ConsoleKey.Escape)
                        {
                            selecting = false;
                        }
                        break;
                    case ConsoleKey.Backspace:
                        selecting = false;
                        break;
                }
            }
        }


        private void ShowCart(Stack<int> menuStack)
        {
            int selectedIndex = 0;
            bool viewingCart = true;

            while (viewingCart)
            {
                _view.ShowCartItems(_cart.GetItems(), selectedIndex);
                Console.WriteLine($"Wartość zakupów: {_cart.GetTotal():C}");

                if (_cart.GetItems().Count == 0)  // Jeśli koszyk jest pusty, blokujemy dodawanie/usuwanie
                {
                    Console.WriteLine("Koszyk jest pusty. Nie możesz dodać ani usunąć produktów.");
                }

                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = selectedIndex == 0 ? _cart.GetItems().Count - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = selectedIndex == _cart.GetItems().Count - 1 ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        if (_cart.GetItems().Count == 0)  // Jeśli koszyk jest pusty, nie pozwalamy na wybór
                        {
                            Console.Clear();
                            Console.WriteLine("Koszyk jest pusty, nie możesz dodać ani usunąć produktów.");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("1. Dodaj więcej sztuk");
                            Console.WriteLine("2. Usuń sztuki");
                            int action = _view.GetIntInput(); // Pobieranie akcji od użytkownika

                            if (action == 1)
                            {
                                int quantity = _view.GetProductQuantityInput("dodać");
                                _cart.AddProduct(_cart.GetItems()[selectedIndex].Product, quantity);
                            }
                            else if (action == 2)
                            {
                                int quantity = _view.GetProductQuantityInput("usunąć");
                                _cart.RemoveProduct(_cart.GetItems()[selectedIndex].Product, quantity);
                            }
                        }

                        if (_cart.GetItems().Count == 0)
                        {
                            viewingCart = false;  // Jeśli koszyk jest pusty, wychodzimy
                        }
                        break;
                    case ConsoleKey.Backspace:
                        viewingCart = false;
                        break;
                }
            }
        }



        private void RemoveProductFromCart(Stack<int> menuStack)
        {
            int selectedIndex = 0;
            bool selecting = true;

            if (_cart.GetItems().Count == 0)
            {
                Console.WriteLine("Koszyk jest pusty.");
                Console.ReadKey();
                return;
            }

            while (selecting)
            {
                _view.ShowCartItems(_cart.GetItems(), selectedIndex);
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = selectedIndex == 0 ? _cart.GetItems().Count - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = selectedIndex == _cart.GetItems().Count - 1 ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        int quantity = _view.GetProductQuantityInput("usunąć");
                        _cart.RemoveProduct(_cart.GetItems()[selectedIndex].Product, quantity);
                        break;
                    case ConsoleKey.Backspace:
                        selecting = false;
                        break;
                }
            }
        }

        private void FinalizeOrder()
        {
            if (_cart.GetItems().Count == 0)
            {
                Console.WriteLine("Koszyk jest pusty.");
                Console.ReadKey();
                return;
            }

            _view.ShowCartItems(_cart.GetItems(), -1);
            Console.WriteLine($"Do zapłaty: {_cart.GetTotal():C}");
            string address = _view.GetAddressInput();
            Console.WriteLine($"Dziękujemy za zamówienie! Produkty zostaną wysłane na adres: {address}");
            _cart.ClearCart();
            Console.ReadKey();
        }
    }
}
