using CustomerAppBLL.BusinessObjects;
using CustomerAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAppBLL.Converters
{
    class AddressConverter
    {
        internal Address Convert(AddressBO address)
        {
            if (address == null) { return null; }
            return new Address()
            {
                Id = address.Id,
                City = address.City,
                Number = address.Number,
                Street = address.Street
            };
        }

        internal AddressBO Convert(Address address)
        {
            if (address == null) { return null; }
            return new AddressBO()
            {
                Id = address.Id,
                City = address.City,
                Number = address.Number,
                Street = address.Street
            };
        }
    }
}
