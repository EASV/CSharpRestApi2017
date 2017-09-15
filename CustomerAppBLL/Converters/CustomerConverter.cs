using CustomerAppBLL.BusinessObjects;
using CustomerAppDAL.Entities;
using System.Linq;

namespace CustomerAppBLL.Converters
{
    class CustomerConverter
    {
        private AddressConverter aConv;

        public CustomerConverter()
        {
            aConv = new AddressConverter();
        }

        internal Customer Convert(CustomerBO cust)
        {
            if (cust == null) { return null; }
            return new Customer()
            {
                Id = cust.Id,
                Addresses = cust.Addresses?.Select(a => new CustomerAddress() {
                    AddressId = a.Id,
                    CustomerId = cust.Id
                }).ToList(),
                FirstName = cust.FirstName,
                LastName = cust.LastName
            };
        }

        internal CustomerBO Convert(Customer cust)
        {
			if (cust == null) { return null; }
            return new CustomerBO()
            {
                Id = cust.Id,
                Addresses = cust.Addresses?.Select(a => new AddressBO() {
                    Id = a.CustomerId,
                    City = a.Address?.City,
                    Number = a.Address?.Number,
                    Street = a.Address?.Street
                }).ToList(),
                FirstName = cust.FirstName,
                LastName = cust.LastName 
            };
        }
    }
}
