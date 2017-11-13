using CustomerAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAppDAL
{
    public interface IOrderRepository
    {
        //C
        Order Create(Order cust);
        //R
        IEnumerable<Order> GetAll();
        Order Get(int Id);
        //U
        //No Update for Repository, It will be the task of Unit of Work
        //D
        Order Delete(int Id);
    }
}
