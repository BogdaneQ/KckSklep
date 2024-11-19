using System;
using System.Collections.Generic;
using Xunit;
using Moq;

namespace KckTests
{
    public class CartTests
    {
        [Fact]
        public void AddProduct_Should_Add_Product_To_Cart()
        {
            // Arrange
            var cart = new Cart();
            var product = new Product(1, "Pi³ka no¿na", 100.99m, 10, "Opis", "Pi³ki");

            // Act
            cart.AddProduct(product, 2);

            // Assert
            Assert.Single(cart.GetItems());
            Assert.Equal(2, cart.GetItems()[0].Quantity);
            Assert.Equal(product, cart.GetItems()[0].Product);
        }

        [Fact]
        public void AddProduct_Should_Increase_Quantity_When_Product_Already_Exists()
        {
            // Arrange
            var cart = new Cart();
            var product = new Product(1, "Pi³ka no¿na", 100.99m, 10, "Opis", "Pi³ki");
            cart.AddProduct(product, 2);

            // Act
            cart.AddProduct(product, 3);

            // Assert
            Assert.Single(cart.GetItems());
            Assert.Equal(5, cart.GetItems()[0].Quantity);
        }

        [Fact]
        public void RemoveProduct_Should_Decrease_Quantity_Or_Remove_Product()
        {
            // Arrange
            var cart = new Cart();
            var product = new Product(1, "Pi³ka no¿na", 100.99m, 10, "Opis", "Pi³ki");
            cart.AddProduct(product, 5);

            // Act
            cart.RemoveProduct(product, 3);

            // Assert
            Assert.Single(cart.GetItems());
            Assert.Equal(2, cart.GetItems()[0].Quantity);

            // Act
            cart.RemoveProduct(product, 2);

            // Assert
            Assert.Empty(cart.GetItems());
        }

        [Fact]
        public void RemoveProduct_Should_Not_Remove_Product_When_Quantity_Is_Zero()
        {
            // Arrange
            var cart = new Cart();
            var product = new Product(1, "Pi³ka no¿na", 100.99m, 10, "Opis", "Pi³ki");
            cart.AddProduct(product, 2);

            // Act
            cart.RemoveProduct(product, 5);

            // Assert
            Assert.Single(cart.GetItems());
            Assert.Equal(2, cart.GetItems()[0].Quantity);
        }

        [Fact]
        public void GetTotal_Should_Return_Correct_Sum()
        {
            // Arrange
            var cart = new Cart();
            var product1 = new Product(1, "Pi³ka no¿na", 100.99m, 10, "Opis", "Pi³ki");
            var product2 = new Product(2, "Koszulka sportowa", 89.99m, 5, "Opis", "Odzie¿");
            cart.AddProduct(product1, 2);
            cart.AddProduct(product2, 1);

            // Act
            var total = cart.GetTotal();

            // Assert
            Assert.Equal(291.97m, total);
        }

        [Fact]
        public void ClearCart_Should_Remove_All_Products_From_Cart()
        {
            // Arrange
            var cart = new Cart();
            var product1 = new Product(1, "Pi³ka no¿na", 100.99m, 10, "Opis", "Pi³ki");
            var product2 = new Product(2, "Koszulka sportowa", 89.99m, 5, "Opis", "Odzie¿");
            cart.AddProduct(product1, 2);
            cart.AddProduct(product2, 1);

            // Act
            cart.ClearCart();

            // Assert
            Assert.Empty(cart.GetItems());
        }

        [Fact]
        public void GetTotal_Should_Return_Zero_When_Cart_Is_Empty()
        {
            // Arrange
            var cart = new Cart();

            // Act
            var total = cart.GetTotal();

            // Assert
            Assert.Equal(0m, total);
        }

    }

    public class CartValidationTests
    {
        [Fact]
        public void AddProduct_Should_Throw_Exception_When_Product_Is_Null()
        {
            // Arrange
            var cart = new Cart();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => cart.AddProduct(null, 1));
        }

        [Fact]
        public void AddProduct_Should_Throw_Exception_When_Quantity_Is_Less_Than_One()
        {
            // Arrange
            var cart = new Cart();
            var product = new Product(1, "Pi³ka no¿na", 100.99m, 10, "Opis", "Pi³ki");

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => cart.AddProduct(product, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => cart.AddProduct(product, -1));
        }

        [Fact]
        public void RemoveProduct_Should_Throw_Exception_When_Product_Is_Null()
        {
            // Arrange
            var cart = new Cart();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => cart.RemoveProduct(null, 1));
        }

        [Fact]
        public void RemoveProduct_Should_Throw_Exception_When_Quantity_Is_Less_Than_One()
        {
            // Arrange
            var cart = new Cart();
            var product = new Product(1, "Pi³ka no¿na", 100.99m, 10, "Opis", "Pi³ki");
            cart.AddProduct(product, 5);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => cart.RemoveProduct(product, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => cart.RemoveProduct(product, -1));
        }

        [Fact]
        public void GetTotal_Should_Throw_Exception_When_Cart_Has_Invalid_Products()
        {
            // Arrange
            var cart = new Cart();
            var invalidProduct = new Product(1, null, 100.99m, 10, "Opis", "Pi³ki");
            cart.AddProduct(invalidProduct, 2);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => cart.GetTotal());
        }
    }
}
