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
        public bool PlaceTheOrder(List<CartModel> orderdetails)
        {
            bool res=false;
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {
                    foreach (var order in orderdetails)
                    {
                        SqlCommand sqlCommand = new SqlCommand("dbo.PlaceTheOrder", sqlConnection);
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("@BookId", order.BookID);
                        sqlCommand.Parameters.AddWithValue("@CartId", order.CartID);
                        sqlCommand.Parameters.AddWithValue("@UserId", order.UserId);
                        string date = DateTime.Now.ToString(" dd MMM yyyy");
                        sqlCommand.Parameters.AddWithValue("@OrderDate", date);
                        var returnedSQLParameter = sqlCommand.Parameters.Add("@result", SqlDbType.Int);
                        returnedSQLParameter.Direction = ParameterDirection.Output;
                        sqlCommand.ExecuteNonQuery();
                        int result = (int)returnedSQLParameter.Value;

                        if (result == 1)
                        {
                            res = true;
                            sqlConnection.Close();
                        }
                        else
                        {
                            res = false;
                            break;
                        }
                    }
                    return res;
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
        public List<OrderModel> GetOrderList(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.GetOrderDetails", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<OrderModel> orderList = new List<OrderModel>();
                        while (reader.Read())
                        {
                            BooksModel booksModel = new BooksModel();
                            OrderModel orderModel = new OrderModel();


                            orderModel.BookId = Convert.ToInt32(reader["BookId"]);
                            booksModel.AuthorName = reader["AuthorName"].ToString();
                            booksModel.BookName = reader["BookName"].ToString();
                            booksModel.Price = Convert.ToInt32(reader["Price"]);
                            booksModel.Image = reader["Image"].ToString();
                            booksModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                            orderModel.OrderId = Convert.ToInt32(reader["OrderId"]);
                            orderModel.DateOfOrder = reader["OrderDate"].ToString();
                            orderModel.Books = booksModel;

                            orderList.Add(orderModel);
                        }
                        return orderList;
                    }
                    else
                    {
                        return null;
                    }
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
