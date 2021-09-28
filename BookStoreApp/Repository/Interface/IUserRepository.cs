using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IUserRepository
    {

        int Register(RegisterModel userDetails);
        RegisterModel Login(LoginModel loginData);
        DataResponseModel ForgetPassword(string email);
    }
}
