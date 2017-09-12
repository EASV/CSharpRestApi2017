using System;
namespace CustomerAppDAL
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        IOrderRepository OrderRepository { get; }
        IAddressRepository AddressRepository { get; }

        int Complete();
    }
}
