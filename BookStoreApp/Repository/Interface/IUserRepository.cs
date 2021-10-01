using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IUserRepository
    {

        bool Register(RegisterModel userDetails);
        RegisterModel Login(LoginModel loginData);

        DataResponseModel ForgetPassword(string email);

        bool ResetPassword(ResetPasswordModel resetPasswordModel);

        public bool AddUserDetails(UserDetailsModel userDetails);
        List<UserDetailsModel>  GetUserDetails(int userId);
        bool EditAddress(UserDetailsModel details);
        bool EditUserDetails(RegisterModel details);

        string GenerateToken(string email);
    }
}
