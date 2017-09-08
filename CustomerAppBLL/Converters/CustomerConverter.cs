using CustomerAppBLL.BusinessObjects;
using CustomerAppDAL.Entities;

namespace CustomerAppBLL.Converters
{
    class CustomerConverter
    {
        internal Customer Convert(CustomerBO cust)
        {
            if (cust == null) { return null; }
            return new Customer()
            {
                Id = cust.Id,
                Address = cust.Address,
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
                Address = cust.Address,
                FirstName = cust.FirstName,
                LastName = cust.LastName
            };
        }
    }
}
