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
        public AdminModel AdminLogin(LoginModel loginData);
        bool ResetPassword(ResetPasswordModel resetPasswordModel);
        bool EditUserDetails(RegisterModel details);

        string GenerateToken(string email);
    }
}
