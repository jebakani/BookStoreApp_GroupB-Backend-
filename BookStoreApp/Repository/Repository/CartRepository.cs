using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Model;
using System.Data;
using Repository.Interface;

namespace Repository.Repository
{
    public class CartRepository:ICartRepository
    {
        public CartRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;

        public bool AddToCart(CartModel details)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.AddToCart", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookId",details.BookID);
                    sqlCommand.Parameters.AddWithValue("@UserId", details.UserId);
                    sqlCommand.Parameters.AddWithValue("@NoOfBook", details.BookOrderCount);
                    var returnedSQLParameter = sqlCommand.Parameters.Add("@result", SqlDbType.Int);
                    returnedSQLParameter.Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    var result = (int)returnedSQLParameter.Value;
                    if (result.Equals(1))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
        }
        public bool DeleteFromCart(int cartId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.RemoveFromCart", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@cartId", cartId);
                    var returnedSQLParameter = sqlCommand.Parameters.Add("@result", SqlDbType.Int);
                    returnedSQLParameter.Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    var result = (int)returnedSQLParameter.Value;
                    if (result.Equals(1))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
        }
        public List<CartModel> GetCartItems(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.GetCartItem", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    List<CartModel> cartItems = new List<CartModel>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CartModel cart = new CartModel();
                            BooksModel book = new BooksModel();
                            cart.BookID = Convert.ToInt32(reader[0]);
                            book.AuthorName = reader[1].ToString();
                            book.BookName = reader[2].ToString();
                            book.Price = Convert.ToInt32(reader[3]);
                            book.Image = reader[8].ToString();
                            book.OriginalPrice = Convert.ToInt32(reader[4]);
                            book.BookCount = Convert.ToInt32(reader[7]);
                            cart.CartID = Convert.ToInt32(reader[5]);
                            cart.BookOrderCount= Convert.ToInt32(reader[6]);
                            cart.Books = book;
                            cartItems.Add(cart);
                        }
                        
                    }
                    return cartItems;
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
        public bool UpdateOrderCount(CartModel cartDetail)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.EditNumberOfBooks", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@CartId", cartDetail.CartID);
                    sqlCommand.Parameters.AddWithValue("@type", cartDetail.orderCountType);
                    var returnedSQLParameter = sqlCommand.Parameters.Add("@result", SqlDbType.Int);
                    returnedSQLParameter.Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    var result = (int)returnedSQLParameter.Value;
                    if(result.Equals(1))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
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
