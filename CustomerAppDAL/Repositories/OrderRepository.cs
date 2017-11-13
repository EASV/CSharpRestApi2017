using System;
using System.Collections.Generic;
using System.Text;
using CustomerAppDAL.Entities;
using CustomerAppDAL.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CustomerAppDAL.Repositories
{
    class OrderRepository : IOrderRepository
    {
        CustomerAppContext _context;
        public OrderRepository(CustomerAppContext context)
        {
            _context = context;
        }

        public Order Create(Order order)
        {
            _context.Orders.Add(order);
             return order;
        }

        public Order Delete(int Id)
        {
            var order = Get(Id);
            _context.Orders.Remove(order);
            return order;
        }

        public Order Get(int Id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == Id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }
    }
}
