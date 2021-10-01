using Manager.Inteface;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Repository
{

    public class BookRepository : IBookRepository
    {
        public BookRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;


        public List<BooksModel> GetAllBooks()
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
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
                            booksModel.BookId = Convert.ToInt32(reader["BookId"]);
                            booksModel.AuthorName = reader["AuthorName"].ToString();
                            booksModel.BookName = reader["BookName"].ToString();
                            booksModel.BookDescription = reader["BookDescription"].ToString();
                            booksModel.Price = Convert.ToInt32(reader["Price"]);
                            booksModel.Image = reader["Image"].ToString();
                            booksModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
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
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
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
        public BooksModel GetBookDetail(int bookId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.GetBook", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                    BooksModel booksModel = new BooksModel();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        booksModel.AuthorName = reader["AuthorName"].ToString();
                        booksModel.BookName = reader["BookName"].ToString();
                        booksModel.BookDescription = reader["BookDescription"].ToString();
                        booksModel.Price = Convert.ToInt32(reader["Price"]);
                        booksModel.Image = reader["Image"].ToString();
                        booksModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                        booksModel.BookCount = Convert.ToInt32(reader["BookCount"]);
                        booksModel.Rating = Convert.ToInt32(reader["Rating"]);
                    }
                    return booksModel;
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

        public bool AddCustomerFeedBack(FeedbackModel feedbackModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {

                    SqlCommand sqlCommand = new SqlCommand("dbo.AddFeedback", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookId", feedbackModel.bookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", feedbackModel.userId);
                    sqlCommand.Parameters.AddWithValue("@Rating", feedbackModel.rating);
                    sqlCommand.Parameters.AddWithValue("@FeedBack", feedbackModel.feedback);


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
