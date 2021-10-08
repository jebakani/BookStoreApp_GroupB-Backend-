using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Inteface
{
         public interface IAddressManager
        {
            public bool AddUserAddress(AddressModel userDetails);
            public bool RemoveFromUserDetails(int addressId);
            List<AddressModel> GetUserDetails(int userId);
            bool EditAddress(AddressModel details);
        }
    
}
