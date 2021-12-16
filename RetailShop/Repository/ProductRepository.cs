using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using RetailShop.Common;
using RetailShop.Model;

namespace RetailShop.Repository
{
    class ProductRepository : IProductRepository
    {
        private readonly RetailShopDbContext _retailShopDbContext;

        internal ProductRepository(RetailShopDbContext retailShopDbContext)
        {
            _retailShopDbContext = retailShopDbContext;
        }

        public List<Product> GetProducts()
        {
            return _retailShopDbContext.Products.ToList();
        }

        public Product GetProduct(int productId)
        {
            return _retailShopDbContext.Products.FirstOrDefault(x => x.ProductId == productId);
        }

        public void Add(Product product)
        {
            _retailShopDbContext.Products.Add(product);
            _retailShopDbContext.SaveChanges();
        }

        public void Update(int productId, Product product)
        {
           var p = _retailShopDbContext.Products.FirstOrDefault(x => x.ProductId == product.ProductId);

           if (p != null)
           {
               p.Price = product.Price;
               p.Quantity = product.Quantity;
               p.ProductName = product.ProductName;
               _retailShopDbContext.SaveChanges();
           }
          
        }

        public void Delete(int productId)
        {
            var product = _retailShopDbContext.Products.SingleOrDefault(x => x.ProductId == productId);
            
            if (product != null)
            {
                _retailShopDbContext.Products.Remove(product);
                _retailShopDbContext.SaveChanges();
            }
            
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var product = _retailShopDbContext.Products.FirstOrDefault(x => x.ProductId == productId);

            if (product != null)
            {
                product.Quantity = quantity;
                _retailShopDbContext.SaveChanges();
            }
        }

        public void UpdatePrice(int productId, double price)
        {
            var product = _retailShopDbContext.Products.FirstOrDefault(x => x.ProductId == productId);

            if (product != null)
            {
                product.Price = price;
                _retailShopDbContext.SaveChanges();
            }
        }
    }

    interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProduct(int productId);
        void Add(Product product);
        void Update(int productId, Product product);
        void Delete(int productId);
        void UpdateQuantity(int productId, int quantity);
        void UpdatePrice(int productId, double price);

    }
}
