using CustomerAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAppDAL
{
    public interface IAddressRepository
    {
        //C
        Address Create(Address address);
        //R
        IEnumerable<Address> GetAll();
        IEnumerable<Address> GetAllById(List<int> ids);
        Address Get(int Id);
        //U
        //No Update for Repository, It will be the task of Unit of Work
        //D
        Address Delete(int Id);
    }
}
