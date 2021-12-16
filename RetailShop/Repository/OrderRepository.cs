using System.Collections.Generic;
using System.Linq;
using RetailShop.Common;
using RetailShop.Model;

namespace RetailShop.Repository
{
    class OrderRepository : IOrderRepository
    {
        private readonly RetailShopDbContext _retailShopDbContext;

        internal OrderRepository(RetailShopDbContext retailShopDbContext)
        {
            _retailShopDbContext = retailShopDbContext;
        }

        public List<Order> GetOrders()
        {
            return _retailShopDbContext.Orders.ToList();
        }

        public Order GetOrder(int orderId)
        {
            return _retailShopDbContext.Orders.FirstOrDefault(x => x.OrderId == orderId);
        }

        public void Place(Order order)
        {
            _retailShopDbContext.Orders.Add(order);
            _retailShopDbContext.SaveChanges();
        }

        public void Update(int orderId, Order order)
        {
            var o = _retailShopDbContext.Orders.FirstOrDefault(x => x.OrderId == order.OrderId);

            if (order != null)
            {
                o.DateTime = order.DateTime;
                o.Quantity = order.Quantity;
                _retailShopDbContext.SaveChanges();
            }
        }

        public void Cancel(int orderId)
        {
            var order = _retailShopDbContext.Orders.FirstOrDefault(x => x.OrderId == orderId);

            if (order != null)
            {
                _retailShopDbContext.Orders.Remove(order);
                _retailShopDbContext.SaveChanges();
            }

        }

    }

    interface IOrderRepository
    {
        List<Order> GetOrders();
        Order GetOrder(int orderId);
        void Place(Order order);
        void Update(int orderId, Order order);
        void Cancel(int orderId);

    }
}
