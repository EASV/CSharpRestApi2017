using System;
using System.Collections.Generic;
using System.Text;
using CustomerAppDAL;
using System.Linq;
using CustomerAppDAL.Entities;
using CustomerAppBLL.BusinessObjects;
using CustomerAppBLL.Converters;

namespace CustomerAppBLL.Services
{
    class CustomerService : ICustomerService
    {
        CustomerConverter conv = new CustomerConverter();
        AddressConverter aConv = new AddressConverter();
        DALFacade facade;
        public CustomerService(DALFacade facade)
        {
            this.facade = facade;
        }
        
        public CustomerBO Create(CustomerBO cust)
        {
            using(var uow = facade.UnitOfWork)
            {
				var newCust = uow.CustomerRepository.Create(conv.Convert(cust));
				uow.Complete();
				return conv.Convert(newCust);
            }
        }

        public void CreateAll(List<CustomerBO> customers)
        {
            using (var uow = facade.UnitOfWork)
            {
                foreach (var customer in customers)
                {
                    uow.CustomerRepository.Create(conv.Convert(customer));
                    
                }
                uow.Complete();
            }
        }

        public CustomerBO Delete(int Id)
        {
			using (var uow = facade.UnitOfWork)
			{
				var newCust = uow.CustomerRepository.Delete(Id);
				uow.Complete();
				return conv.Convert(newCust);
			}
        }

        public CustomerBO Get(int Id)
        {
            using (var uow = facade.UnitOfWork)
			{
                //1. Get and convert the customer
                var cust = conv.Convert(uow.CustomerRepository.Get(Id));

                //2. Get All related Addresses from AddressRepository using addressIds
                //3. Convert and Add the Addresses to the CustomerBO

                /*cust.Addresses = cust.AddressIds?
                    .Select(id => aConv.Convert(uow.AddressRepository.Get(id)))
                    .ToList();*/

                cust.Addresses = uow.AddressRepository.GetAllById(cust.AddressIds)
                    .Select(a => aConv.Convert(a))
                    .ToList();

                //4. Return the Customer
                return cust;
            }
        }

        public List<CustomerBO> GetAll()
        {
			using (var uow = facade.UnitOfWork)
			{
                //Customer -> CustomerBO
                //return uow.CustomerRepository.GetAll();
                return uow.CustomerRepository.GetAll().Select(conv.Convert).ToList();
			}
        }

        public List<CustomerBO> 
        GetAllByFirstName(string t, int ps, int cp)
        {
            using (var uow = facade.UnitOfWork){
                var skip = (ps * cp) - ps;
                return uow.CustomerRepository
                          .GetAll()
                          .Where(c => c.FirstName.Contains(t))
                          .Skip(skip)
                          .Take(ps)
                          .Select(c => conv.Convert(c))
                          .ToList();
            }
        }

        public CustomerBO Update(CustomerBO cust)
        {
            using (var uow = facade.UnitOfWork)
            {
                var customerFromDb = uow.CustomerRepository.Get(cust.Id);
				if (customerFromDb == null)
				{
					throw new InvalidOperationException("Customer not found");
				}

                var customerUpdated = conv.Convert(cust);
				customerFromDb.FirstName = customerUpdated.FirstName;
				customerFromDb.LastName = customerUpdated.LastName;

                //1. Remove All, except the "old" ids we 
                //      wanna keep (Avoid attached issues)
                customerFromDb.Addresses.RemoveAll(
                    ca => !customerUpdated.Addresses.Exists(
                        a => a.AddressId == ca.AddressId &&
                        a.CustomerId == ca.CustomerId));

                //2. Remove All ids already in database 
                //      from customerUpdated
                customerUpdated.Addresses.RemoveAll(
                    ca => customerFromDb.Addresses.Exists(
                        a => a.AddressId == ca.AddressId &&
                        a.CustomerId == ca.CustomerId));

                //3. Add All new CustomerAddresses not 
                //      yet seen in the DB
                customerFromDb.Addresses.AddRange(
                    customerUpdated.Addresses);

                uow.Complete();
				return conv.Convert(customerFromDb);
            }

        }

    }
}
