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
                Addresses = cust.AddressIds?.Select(aId => new CustomerAddress() {
                    AddressId = aId,
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
                AddressIds = cust.Addresses?.Select(a => a.AddressId).ToList(),
                FirstName = cust.FirstName,
                LastName = cust.LastName 
            };
        }
    }
}
