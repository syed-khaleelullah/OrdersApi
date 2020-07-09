using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevopsDemoOrdersApi.Models
{
    public class OrdersRepository
    {
        static List<Order> Orders;
        static OrdersRepository()
        {
            Orders = new List<Order>();
            Orders.Add(new Order()
            {
                OrderId = 1234,
                CustomerName = "Jojo",
                DeliveryAddress = "Hyderabad",
                OrderAmount = 200000,
                ProductId = 102
            });
        }
        public Order FindById(int orderId)
        {
            return Orders.Find(o => o.OrderId == orderId);
        }
    }
}
