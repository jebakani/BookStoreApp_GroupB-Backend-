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
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookStore;";

        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public int Register(RegisterModel userDetails)
        {
            using (sqlConnection)

                try
                {
                    var password = this.EncryptPassword(userDetails.Password);
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertIntoUsers", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@FullName", userDetails.CustomerName);
                    sqlCommand.Parameters.AddWithValue("@EmailId", userDetails.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", password);
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
        public string EncryptPassword(string password)
        {
            ////encodes Unicode characters into a sequence of one to four bytes per character
            var passwordInBytes = Encoding.UTF8.GetBytes(password);

            ////Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits
            string encodedPassword = Convert.ToBase64String(passwordInBytes);

            ////returns the encoded pasword
            return encodedPassword;
        }
        public RegisterModel Login(LoginModel loginData)
        {
            try
            {
                var encodedpassword = this.EncryptPassword(loginData.Password);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[UserLogin]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmailId", loginData.Email);
                cmd.Parameters.AddWithValue("@Password", encodedpassword);
                var returnedSQLParameter = cmd.Parameters.Add("@result", SqlDbType.Int);
                returnedSQLParameter.Direction = ParameterDirection.ReturnValue;

                RegisterModel customer = new RegisterModel();
                SqlDataReader rd = cmd.ExecuteReader();
                //var result = (int)returnedSQLParameter.Value;

                //if ( result.Equals(3))
                //{
                //    throw new Exception("Email not registered");
                //}
                if (rd.Read())
                {
                    customer.CustomerId = rd["userId"] == DBNull.Value ? default : rd.GetInt32("userId");
                    customer.CustomerName = rd["FullName"] == DBNull.Value ? default : rd.GetString("FullName");
                    customer.PhoneNumber = rd["Phone"] == DBNull.Value ? default : rd.GetString("Phone");
                    customer.Email = rd["EmailId"] == DBNull.Value ? default : rd.GetString("EmailId");
                    customer.Password = rd["Password"] == DBNull.Value ? default : rd.GetString("Password");
                }
                return customer;
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

    }

}
