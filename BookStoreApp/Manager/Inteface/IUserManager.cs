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

    }
}
