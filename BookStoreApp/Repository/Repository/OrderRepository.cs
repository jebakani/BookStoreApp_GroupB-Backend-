using Microsoft.Extensions.Configuration;
using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Repository
{
    /// <summary>
    /// Order repository 
    /// </summary>
    /// <seealso cref="Repository.Interface.IOrderRepository" />
    public class OrderRepository:IOrderRepository
    {
        public OrderRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;
        public bool PlaceTheOrder(CartModel orderdetails)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {

                    SqlCommand sqlCommand = new SqlCommand("dbo.PlaceTheOrder", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookId", orderdetails.BookID);
                    sqlCommand.Parameters.AddWithValue("@CartId", orderdetails.CartID);
                    sqlCommand.Parameters.AddWithValue("@UserId", orderdetails.UserId);
                    string date = DateTime.Now.ToString(" dd MMM yyyy");
                    sqlCommand.Parameters.AddWithValue("@OrderDate", date);
                    var returnedSQLParameter = sqlCommand.Parameters.Add("@result", SqlDbType.Int);
                    returnedSQLParameter.Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    int result = (int)returnedSQLParameter.Value;
                    if (result == 1)
                        return true;
                    else
                        return false;

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
