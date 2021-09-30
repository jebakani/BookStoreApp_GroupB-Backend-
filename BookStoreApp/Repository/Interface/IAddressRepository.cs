using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IAddressRepository
    {
         bool AddUserDetails(AddressModel userDetails);
         bool RemoveFromUserDetails(int addressId);
        List<AddressModel> GetUserDetails(int userId);
        bool EditAddress(AddressModel details);
    }
}
