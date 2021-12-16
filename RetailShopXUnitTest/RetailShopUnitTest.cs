using RetailShop.Model;
using RetailShop.Services;
using Xunit;

namespace RetailShopXUnitTest
{
    public class RetailShopUnitTest
    {
        private readonly RetailShopManager _retailShopManager;

        public RetailShopUnitTest()
        {
            _retailShopManager = new RetailShopManager();
            Seed();
        }

        private void Seed()
        {
            Product product = new Product { ProductId = 1, Price = 100, ProductName = "test", Quantity = 10 };
            _retailShopManager.AddOrUpdateProduct(product);

            product = new Product { ProductId = 2, Price = 200, ProductName = "test2", Quantity = 20 };
            _retailShopManager.AddOrUpdateProduct(product);
        }

        [Fact]
        public void Test_AddOrUpdateProduct()
        {
            Product product = new Product {ProductId = 1, Price = 100, ProductName = "test", Quantity = 10};
            _retailShopManager.AddOrUpdateProduct(product);

            product = new Product {ProductId = 2, Price = 200, ProductName = "test2", Quantity = 20};
            _retailShopManager.AddOrUpdateProduct(product);

            product.ProductName = "test_modified";
            _retailShopManager.UpdateProduct(2, product);

            var product1 = _retailShopManager.GetProduct(1);
            var product2 = _retailShopManager.GetProduct(2);

            Assert.True(product1.ProductId == 1);
            Assert.True(product2.ProductId == 2);
            Assert.True(product2.ProductName.Equals("test_modified"));
        }

        [Fact]
        public void Test_GetAvailableProducts()
        {
            Product product = new Product {ProductId = 1, Price = 100, ProductName = "test", Quantity = 0};
            _retailShopManager.AddOrUpdateProduct(product);
            
            var product1 = _retailShopManager.GetAvailableProducts();

            Assert.True(product1.Count == 1);
        }

        [Fact]
        public void Test_DeleteProduct()
        {
            _retailShopManager.DeleteProduct(1);

            var product = _retailShopManager.GetProduct(1);

            Assert.True(product == null);

            Seed();
        }

        [Fact]
        public void Test_PlaceOrder()
        {
            _retailShopManager.PlaceOrder(1, 5);

            var product = _retailShopManager.GetProduct(1);

            Assert.True(product.Quantity == 5);
        }

        [Fact]
        public void Test_CancelOrder()
        {
            var product = _retailShopManager.GetProduct(1);
            product.Quantity = 10;
            _retailShopManager.UpdateProduct(product.ProductId, product);
            _retailShopManager.PlaceOrder(1, 5);

            _retailShopManager.CancelOrder(1);

            product = _retailShopManager.GetProduct(1);

            Assert.True(product.Quantity == 10);
        }
    }
}
