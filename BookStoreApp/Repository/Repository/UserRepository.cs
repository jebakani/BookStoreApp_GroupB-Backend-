using Model;
using Repository.Interface;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using Experimental.System.Messaging;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;


namespace Repository.Repository
{


    public class UserRepository : IUserRepository
    {
        
        public UserRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public  IConfiguration Configuration { get; }
        SqlConnection sqlConnection;
        public bool Register(RegisterModel userDetails)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
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
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
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
                returnedSQLParameter.Direction = ParameterDirection.Output;

                RegisterModel customer = new RegisterModel();
                SqlDataReader rd = cmd.ExecuteReader();
                //var result = (int)returnedSQLParameter.Value;

                //if ( result.Equals(3))
                //{
                //    throw new Exception("Email not registered");
                //}
                if (rd.Read())
                {
                    customer.CustomerId = rd.GetInt32("userId");
                    customer.CustomerName =rd.GetString("FullName");
                    customer.PhoneNumber = rd.GetInt64("Phone").ToString();
                    customer.Email =rd.GetString("EmailId");
                    customer.Password =rd.GetString("Password");
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
        public DataResponseModel ForgetPassword(string email)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[EmailValidity]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmailId", email);
                cmd.Parameters.Add("@result", SqlDbType.Int);
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@userId", SqlDbType.Int);
                cmd.Parameters["@userId"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                var result = cmd.Parameters["@result"].Value;

                if (result != null&& result.Equals(1))
                {
                    var userId = Convert.ToInt32( cmd.Parameters["@userId"].Value);
                    Random random = new Random();
                    int OTP = random.Next(1000, 9999);
                    this.MSMQSend(OTP);
                    if (this.SendEmail(email))
                    {
                        return new DataResponseModel(){ CustomerId = userId, message = "Otp is send to Email", otp = OTP };
                    }
                    else
                    {
                        return new DataResponseModel() {  message = "Sent email failed" };
                    }
                }
                else
                {
                    return new DataResponseModel() { message = "Invalid email id" };

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// method to create new queue for message or get the queue if already exists
        /// </summary>
        /// <returns>the queue</returns>
        private MessageQueue QueueDetail()
        {
            MessageQueue messageQueue;
            if (MessageQueue.Exists(@".\Private$\ResetPasswordQueue"))
            {
                messageQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\ResetPasswordQueue");
            }
        
            return messageQueue;
        }
        private bool SendEmail(string email)
        {
            string linkToBeSend = this.ReceiveQueue(email);
            if (this.SendMailUsingSMTP(email, linkToBeSend))
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// method send the message in the queue
        /// </summary>
        /// <param name="url">url link that has to be send</param>
        private void MSMQSend(int otp)
        {
            try
            {
                MessageQueue messageQueue = this.QueueDetail();
                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = otp;
                messageQueue.Label = "Otp for password reset";
                messageQueue.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// method to get the message from the queue and send it to the mail
        /// </summary>
        /// <param name="email">email id of user to send mail</param>
        /// <returns>returns whether the mail is send or not</returns>
        private string ReceiveQueue(string email)
        {
            ////for reading from MSMQ
            var receiveQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            var receiveMsg = receiveQueue.Receive();
            receiveMsg.Formatter = new BinaryMessageFormatter();

            string linkToBeSend = receiveMsg.Body.ToString();
            return linkToBeSend;
        }

        /// <summary>
        /// method to send the mail
        /// </summary>
        /// <param name="email">email as string</param>
        /// <param name="message">message can be string or url or combination of both</param>
        /// <returns>returns the result to receive queue method</returns>
        private bool SendMailUsingSMTP(string email, string message)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mailMessage.From = new MailAddress("17cse12jebakaniishwaryav@gmail.com");
            mailMessage.To.Add(new System.Net.Mail.MailAddress(email));
            mailMessage.Subject = "Link to reset you password for BookStore Application";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body ="Here is the otp : "+message;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("17cse12jebakaniishwaryav@gmail.com", "");
            smtp.Send(mailMessage);
            return true;
        }
        public bool ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {
                    //passing query in terms of stored procedure
                    SqlCommand sqlCommand = new SqlCommand("[dbo].[UpdatePassword]", sqlConnection);
                    //passing command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    //adding the parameter to the strored procedure
                    var password = this.EncryptPassword(resetPasswordModel.NewPassword);
                    sqlCommand.Parameters.AddWithValue("@UserId", resetPasswordModel.UserId);
                    sqlCommand.Parameters.AddWithValue("@NewPassword", password);
                    //checking the result 
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

        public bool AddUserDetails(UserDetailsModel userDetails)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));

            using (sqlConnection)

                try
                {
                    
                    SqlCommand sqlCommand = new SqlCommand("dbo.UserDetailsInsert", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@address",userDetails.Address);
                    sqlCommand.Parameters.AddWithValue("@city", userDetails.City);
                    sqlCommand.Parameters.AddWithValue("@state", userDetails.State);
                    sqlCommand.Parameters.AddWithValue("@type", userDetails.Type);
                    sqlCommand.Parameters.AddWithValue("@userId", userDetails.UserId);

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
        public List<UserDetailsModel> GetUserDetails(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[GetUSerDetails]" ,sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataReader readData = cmd.ExecuteReader();
                List<UserDetailsModel> userdetaillist = new List<UserDetailsModel>();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        UserDetailsModel userDetail = new UserDetailsModel();
                        userDetail.AddressId = readData.GetInt32("AddressId");
                        userDetail.Address = readData.GetString("address");
                        userDetail.City = readData.GetString("city").ToString();
                        userDetail.State = readData.GetString("state");
                        userDetail.Type = readData.GetString("type");
                        userdetaillist.Add(userDetail);
                    }
                }
                return userdetaillist;
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
        public bool EditAddress(UserDetailsModel userDetails)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));

            using (sqlConnection)

                try
                {

                    SqlCommand sqlCommand = new SqlCommand("dbo.UpdateUserDetails", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@address", userDetails.Address);
                    sqlCommand.Parameters.AddWithValue("@city", userDetails.City);
                    sqlCommand.Parameters.AddWithValue("@state", userDetails.State);
                    sqlCommand.Parameters.AddWithValue("@type", userDetails.Type);
                    sqlCommand.Parameters.AddWithValue("@addressID", userDetails.AddressId);
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
        public bool EditUserDetails(RegisterModel details)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));

            using (sqlConnection)

                try
                {

                    SqlCommand sqlCommand = new SqlCommand("dbo.UpdateUser", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    var password = this.EncryptPassword(details.Password);
                    sqlCommand.Parameters.AddWithValue("@userId", details.CustomerId);
                    sqlCommand.Parameters.AddWithValue("@FullName", details.CustomerName);
                    sqlCommand.Parameters.AddWithValue("@EmailId", details.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", password);
                    sqlCommand.Parameters.AddWithValue("@Phone", details.PhoneNumber);
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





