using Manager.Inteface;
using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }
        public bool Register(RegisterModel userDetails)
        {
            try
            {
                return this.repository.Register(userDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public RegisterModel Login(LoginModel logindata)
        {
            try
            {
                return this.repository.Login(logindata);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                return this.repository.ResetPassword(resetPasswordModel);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DataResponseModel ForgetPassword(string email)
        {

            try
            {
                return this.repository.ForgetPassword(email);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public bool AddUserDetails(UserDetailsModel userDetails)
        {
            try
            {
                return this.repository.AddUserDetails(userDetails);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public UserDetailsModel GetUserDetails(int userId)
        {
            try
            {
                return this.repository.GetUserDetails(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public  bool EditAddress(UserDetailsModel details)
        {
            try
            {
                return this.repository.EditAddress(details);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
