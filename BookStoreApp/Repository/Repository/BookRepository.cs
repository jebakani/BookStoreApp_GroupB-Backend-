using Manager.Inteface;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Repository
{

    public class BookRepository : IBookRepository

    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True";

        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public List<BooksModel> GetAllBooks()
        {
            using (sqlConnection)

                try
                {

                    SqlCommand sqlCommand = new SqlCommand("dbo.GetAllBooks", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<BooksModel> bookList = new List<BooksModel>();
                        while (reader.Read())
                        {
                            BooksModel booksModel = new BooksModel();

                            booksModel.AuthorName = reader["AuthorName"].ToString();
                            booksModel.BookName = reader["BookName"].ToString();
                            booksModel.BookDescription = reader["BookDescription"].ToString();
                            booksModel.Price = Convert.ToInt32(reader["Price"]);
                            booksModel.Image = reader["Image"].ToString();
                            booksModel.OriginalPrice = Convert.ToInt32(reader["OrginalPrice"]);
                            booksModel.BookCount = Convert.ToInt32(reader["BookCount"]);
                            booksModel.Rating = Convert.ToInt32(reader["Rating"]);

                            bookList.Add(booksModel);
                        }
                        return bookList;
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
        public bool AddBook(BooksModel bookDetails)
        {
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertBooks", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@BookName", bookDetails.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", bookDetails.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@Price", bookDetails.Price);
                    sqlCommand.Parameters.AddWithValue("@originalPrice", bookDetails.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", bookDetails.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@Image", bookDetails.Image);
                    sqlCommand.Parameters.AddWithValue("@Rating", bookDetails.Rating);
                    sqlCommand.Parameters.AddWithValue("@BookCount", bookDetails.BookCount);
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
    }
}
