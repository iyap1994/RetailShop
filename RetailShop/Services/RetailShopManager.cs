using System;
using System.Collections.Generic;
using System.Linq;
using RetailShop.Common;
using RetailShop.Model;
using RetailShop.Repository;

namespace RetailShop.Services
{
    public class RetailShopManager
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public RetailShopManager()
        {
            RetailShopDbContext retailShopDbContext = new RetailShopDbContext();
            _productRepository = new ProductRepository(retailShopDbContext);
            _orderRepository = new OrderRepository(retailShopDbContext);
        }

        #region Product methods

        public List<Product> GetAvailableProducts()
        {
            return _productRepository.GetProducts().Where(x => x?.Quantity > 0).ToList();
        }

        public Product GetProduct(int productId)
        {
            return _productRepository.GetProduct(productId);
        }

        public void AddOrUpdateProduct(Product product)
        {
            var p = GetProduct(product.ProductId);

            if (p == null)
            {
                _productRepository.Add(product);
            }
            else
            {
                UpdateProduct(product.ProductId, product);
            }   
        }

        public void UpdateProduct(int productId, Product product)
        {
            _productRepository.Update(productId, product);
        }

        public void DeleteProduct(int productId)
        {
            _productRepository.Delete(productId);
        }

        public void UpdateProductQuantity(int productId, int quantity)
        {
            _productRepository.UpdateQuantity(productId, quantity);
        }

        public void UpdateProductPrice(int productId, double price)
        {
            _productRepository.UpdatePrice(productId, price);
        }


        #endregion

        #region Order methods

        public List<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }

        public Order GetOrder(int orderId)
        {
            return _orderRepository.GetOrder(orderId);
        }

        public bool PlaceOrder(int productId, int quantity)
        {
            var product = _productRepository.GetProduct(productId);

            if (product == null) return false;

            if (product.Quantity >= quantity)
            {
                Order order = new Order(productId, quantity) {OrderId = 1, DateTime = DateTime.Now};
                _orderRepository.Place(order);

                _productRepository.UpdateQuantity(productId, product.Quantity-quantity);

                return true;
            }

            return false;

        }
        
        public void CancelOrder(int orderId)
        {
            var order = _orderRepository.GetOrder(orderId);

            if (order != null)
            {
                _orderRepository.Cancel(orderId);
                var product = _productRepository.GetProduct(order.ProductId);
                product.Quantity += order.Quantity;
                _productRepository.Update(product.ProductId, product);
            }
        }

        #endregion
    }
}
