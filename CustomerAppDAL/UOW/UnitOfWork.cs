using System;
using CustomerAppDAL.Context;
using CustomerAppDAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CustomerAppDAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; internal set; }
        public IOrderRepository OrderRepository { get; internal set; }
        public IAddressRepository AddressRepository { get; internal set; }
        private CustomerAppContext context;

        public UnitOfWork(DbOptions opt)
        {
            DbContextOptions<CustomerAppContext> options;
            if(opt.Environment == "Development" && String.IsNullOrEmpty(opt.ConnectionString)){
                options = new DbContextOptionsBuilder<CustomerAppContext>()
                   .UseInMemoryDatabase("TheDB")
                   .Options;
            }
            else{
                options = new DbContextOptionsBuilder<CustomerAppContext>()
                .UseSqlServer(opt.ConnectionString)
                    .Options;
            }

            context = new CustomerAppContext(options);
            CustomerRepository = new CustomerRepository(context);
            OrderRepository = new OrderRepository(context);
            AddressRepository = new AddressRepository(context);
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
