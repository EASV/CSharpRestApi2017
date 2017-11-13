using System;
namespace CustomerAppBLL
{
    public interface IBLLFacade
    {
        ICustomerService CustomerService { get; }

        IOrderService OrderService { get; }

        IAddressService AddressService { get; }
    }
}
