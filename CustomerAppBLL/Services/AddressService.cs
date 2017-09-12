using CustomerAppBLL.BusinessObjects;
using CustomerAppBLL.Converters;
using CustomerAppDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerAppBLL.Services
{
    public class AddressService : IAddressService
    {
        AddressConverter _conv;
        DALFacade _facade;

        public AddressService(DALFacade facade)
        {
            _facade = facade;
            _conv = new AddressConverter();
        }

        public AddressBO Create(AddressBO address)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var newaddress = uow.AddressRepository.Create(_conv.Convert(address));
                uow.Complete();
                return _conv.Convert(newaddress);
            }
        }
        
        public AddressBO Delete(int Id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var newAddress = uow.AddressRepository.Delete(Id);
                uow.Complete();
                return _conv.Convert(newAddress);
            }
        }

        public AddressBO Get(int Id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                return _conv.Convert(uow.AddressRepository.Get(Id));
            }
        }

        public List<AddressBO> GetAll()
        {
            using (var uow = _facade.UnitOfWork)
            {
                return uow.AddressRepository.GetAll().Select(_conv.Convert).ToList();
            }
        }

        public AddressBO Update(AddressBO address)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var addressFromDb = uow.AddressRepository.Get(address.Id);
                if (addressFromDb == null)
                {
                    throw new InvalidOperationException("Address not found");
                }

                var addressUpdated = _conv.Convert(address);
                addressFromDb.City = addressUpdated.City;
                addressFromDb.Number = addressUpdated.Number;
                addressFromDb.Street = addressUpdated.Street;
                uow.Complete();
                return _conv.Convert(addressFromDb);
            }

        }
    }
}
