using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Inteface
{
    public interface IUserManager

    {
        public bool Register(RegisterModel userDetails);

        RegisterModel Login(LoginModel loginData);

        DataResponseModel ForgetPassword(string email);

        bool ResetPassword(ResetPasswordModel resetPasswordModel);
        public bool AddUserDetails(AddressModel userDetails);
        public bool RemoveFromUserDetails(int addressId);

        List<AddressModel> GetUserDetails(int userId);
        bool EditAddress(AddressModel details);
        bool EditUserDetails(RegisterModel details);

    }
}
