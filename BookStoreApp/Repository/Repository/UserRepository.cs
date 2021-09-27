using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        public static string connectionString = "Server=localhost;Database=master;Trusted_Connection=True;";
       
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public int Register(RegisterModel userDetails)
        {   
            using (sqlConnection) 

            try 
            {
                SqlCommand sqlCommand = new SqlCommand("dbo.InsertIntoUsers", sqlConnection);

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlConnection.Open();

                sqlCommand.Parameters.AddWithValue("@FullName",userDetails.CustomerName);
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
    }
}
