using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Repository
{

    public class UserRepository : IUserRepository
    {
        public static string connectionString = @"Data Source=localhost;Initial Catalog=BookStore;Integrated Security=True";

        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public int Register(RegisterModel userDetails)
        {
            using (sqlConnection)

                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertIntoUsers", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@FullName", userDetails.CustomerName);
                    sqlCommand.Parameters.AddWithValue("@EmailId", userDetails.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", userDetails.Password);
                    sqlCommand.Parameters.AddWithValue("@Phone", userDetails.PhoneNumber);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return 1;
                    else
                        return 0;

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }

        public RegisterModel Login(LoginModel loginData)
        {
            try { 
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[UserLogin]", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("Email", loginData.Email);
            cmd.Parameters.AddWithValue("Password", loginData.Password);
            var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            RegisterModel customer = new RegisterModel();
            SqlDataReader rd = cmd.ExecuteReader();
            var result = returnParameter.Value;

            if (result.Equals(3))
            {
                throw new Exception("Email not registered");
            }
            if (rd.Read())
            {
                customer.CustomerId = rd["userId"] == DBNull.Value ? default : rd.GetInt32("userId");
                customer.CustomerName = rd["FullName"] == DBNull.Value ? default : rd.GetString("FullName");
                customer.PhoneNumber = rd["Phone"] == DBNull.Value ? default : rd.GetInt64("Phone");
                customer.Email = rd["Email"] == DBNull.Value ? default : rd.GetString("Email");
                customer.PhoneNumber = rd["PhoneNumber"] == DBNull.Value ? default : rd.GetInt64("PhoneNumber");
            }
            return customer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
       
    }

}
