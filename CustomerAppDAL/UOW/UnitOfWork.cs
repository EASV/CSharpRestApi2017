using System;
using CustomerAppDAL.Context;
using CustomerAppDAL.Repositories;

namespace CustomerAppDAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; internal set; }
        public IOrderRepository OrderRepository { get; internal set; }
        private CustomerAppContext context;

        public UnitOfWork()
        {
            context = new CustomerAppContext();
            CustomerRepository = new CustomerRepositoryEFMemory(context);
            OrderRepository = new OrderRepository(context);
        }

		public int Complete()
		{
			//The number of objects written to the underlying database.
			return context.SaveChanges();
		}

        public void Dispose()
        {
            context.Dispose();
        }

    }
}
