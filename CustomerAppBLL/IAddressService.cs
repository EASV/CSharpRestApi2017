using CustomerAppBLL.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAppBLL
{
    public interface IAddressService
    {
        //C
        AddressBO Create(AddressBO cust);
        //R
        List<AddressBO> GetAll();
        AddressBO Get(int Id);
        //U
        AddressBO Update(AddressBO cust);
        //D
        AddressBO Delete(int Id);
    }
    
}
