using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Manager.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
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
        public List<FeedbackModel> GetCustomerFeedBack(int bookid)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[GetCustomerFeedback]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@bookid", bookid);
                List<FeedbackModel> feedbackList = new List<FeedbackModel>();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FeedbackModel feedbackdetails = new FeedbackModel();
                        feedbackdetails.userId = reader.GetInt32(0);
                        feedbackdetails.customerName = reader.GetString("FullName");
                        feedbackdetails.feedback = reader.GetString("Feedback");
                        feedbackdetails.rating = reader.GetDouble("Rating");
                        feedbackList.Add(feedbackdetails);
                    }

                }
                return feedbackList;
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
        public string AddImage( IFormFile image)
        {
            try
            {
                Account account = new Account(this.Configuration.GetValue<string>("CloudConfiguration:CloudName"), this.Configuration.GetValue<string>("CloudConfiguration:APIKey"), this.Configuration.GetValue<string>("CloudConfiguration:APISecret"));
                var cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream()),
                };

                var uploadResult = cloudinary.Upload(uploadParams);
                string imagePath = uploadResult.Url.ToString();
                return imagePath;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditBookDetails(BooksModel bookDetails)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));

            using (sqlConnection)

                try
                {

                    SqlCommand sqlCommand = new SqlCommand("dbo.UpdateBook", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookId", bookDetails.BookId);
                    sqlCommand.Parameters.AddWithValue("@BookName", bookDetails.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", bookDetails.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@Price", bookDetails.Price);
                    sqlCommand.Parameters.AddWithValue("@originalPrice", bookDetails.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", bookDetails.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@Image", bookDetails.Image);
                    sqlCommand.Parameters.AddWithValue("@Rating", bookDetails.Rating);
                    sqlCommand.Parameters.AddWithValue("@BookCount", bookDetails.BookCount);
                    sqlCommand.Parameters.Add("@result", SqlDbType.Int);
                    sqlCommand.Parameters["@result"].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    var result = sqlCommand.Parameters["@result"].Value;
                    if (result.Equals(1))
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
        public bool RemoveBookByAdmin(int bookId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {

                    SqlCommand sqlCommand = new SqlCommand("dbo.RemoveBookByAdmin", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();


                    sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                    sqlCommand.Parameters.Add("@result", SqlDbType.Int);
                    sqlCommand.Parameters["@result"].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();

                    var result = sqlCommand.Parameters["@result"].Value;
                    if (result.Equals(1))
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
