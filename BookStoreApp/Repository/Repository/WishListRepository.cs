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
    public class WishListRepository : IWishListRepository
    {
        public WishListRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;

        public bool AddToWishList(WishListModel wishListModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {
                   
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertIntoWishList", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookId", wishListModel.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", wishListModel.UserId);
                    var returnedSQLParameter = sqlCommand.Parameters.Add("@result", SqlDbType.Int);
                    returnedSQLParameter.Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    int result= (int)returnedSQLParameter.Value;
                    if (result==1)
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
        public bool RemoveFromWishList(int wishListId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {

                    SqlCommand sqlCommand = new SqlCommand("dbo.RemoveFromWishList", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();


                    sqlCommand.Parameters.AddWithValue("@WishListId", wishListId);
                 

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
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
        public List<WishListModel> GetFromWishList(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.GetWishList", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<WishListModel> wishList = new List<WishListModel>();
                        while (reader.Read())
                        {
                            BooksModel booksModel = new BooksModel();
                            WishListModel wishListModel = new WishListModel();


                            wishListModel.BookId = Convert.ToInt32(reader["BookId"]);
                            booksModel.AuthorName = reader["AuthorName"].ToString();
                            booksModel.BookName = reader["BookName"].ToString();
                            booksModel.Price = Convert.ToInt32(reader["Price"]);
                            booksModel.Image = reader["Image"].ToString();
                            booksModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                            wishListModel.WishListId = Convert.ToInt32(reader["WishListId"]);
                            wishListModel.Books = booksModel;

                            wishList.Add(wishListModel);
                        }
                        return wishList;
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
