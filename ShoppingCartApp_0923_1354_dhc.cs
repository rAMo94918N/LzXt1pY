// 代码生成时间: 2025-09-23 13:54:26
using System;
using System.Collections.Generic;

namespace ShoppingCartApp
{
    // Represents a product in the shopping cart
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }

    // Represents a shopping cart
    public class ShoppingCart
    {
        private List<Product> products;

        public ShoppingCart()
        {
            products = new List<Product>();
        }

        // Adds a product to the cart
        public void AddProduct(Product product)
        {
            products.Add(product);
            Console.WriteLine($"Added {product.Name} to the cart.");
        }

        // Removes a product from the cart
        public void RemoveProduct(Product product)
        {
            if (products.Remove(product))
            {
                Console.WriteLine($"Removed {product.Name} from the cart.");
            }
            else
            {
                Console.WriteLine($"Failed to remove {product.Name} from the cart.");
            }
        }

        // Calculates the total price of the cart
        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var product in products)
            {
                total += product.Price;
            }
            return total;
        }

        // Displays the cart contents
        public void DisplayCart()
        {
            Console.WriteLine("Shopping Cart: 
");
            foreach (var product in products)
            {
                Console.WriteLine($"Product: {product.Name}, Price: {product.Price}");
            }
            Console.WriteLine($"Total: {CalculateTotal()}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart cart = new ShoppingCart();
            cart.AddProduct(new Product("Milk", 1.50m));
            cart.AddProduct(new Product("Bread", 2.00m));
            cart.AddProduct(new Product("Eggs", 3.00m));

            Console.WriteLine("Displaying cart contents: 
");
            cart.DisplayCart();

            // Example of error handling
            try
            {
                cart.RemoveProduct(new Product("Cheese", 4.50m)); // This should fail
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}