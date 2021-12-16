using System;
using RetailShop.Model;
using RetailShop.Services;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            RetailShopManager retailShopManager = new RetailShopManager();
            Product product = new Product();
            product.ProductId = 1;
            product.Price = 100;
            product.ProductName = "test";
            product.Quantity = 10;
            retailShopManager.AddOrUpdateProduct(product);

            product = new Product();
            product.ProductId = 2;
            product.Price = 200;
            product.ProductName = "test2";
            product.Quantity = 20;
            retailShopManager.AddOrUpdateProduct(product);

            product.ProductName = "test_modified";

            var product1 = retailShopManager.GetProduct(1);
            var product2 = retailShopManager.GetProduct(2);

            Console.Write(product2.ProductName);

        }
    }
}
