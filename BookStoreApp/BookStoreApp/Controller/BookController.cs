
using Manager.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookManager manager;

        public BookController(IBookManager manager)
        {
            this.manager = manager;

        }
        [HttpPost]
        [Route("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            var result = this.manager.GetAllBooks();
            try
            {
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "All Notes", data = result });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Try again" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }
        [HttpPost]
        [Route("AddBook")]
        public IActionResult AddBook([FromBody] BooksModel bookDetails)
        {
            try
            {
                var result = this.manager.AddBook(bookDetails);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added New Book Successfully !" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add new user, Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }
        [HttpGet]
        [Route("GetBookDetail")]
        public IActionResult GetBookDetail(int bookId)
        {
            var result = this.manager.GetBookDetail(bookId);
            try
            {
                if (result.BookId!=0)
                {
                    return this.Ok(new { Status = true, Message = "Book is retrived", data = result });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Try again" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }

        [HttpPost]
        [Route("AddCustomerFeedBack")]
        public IActionResult AddCustomerFeedBack([FromBody] FeedbackModel feedbackModel)
        {
            try
            {
                var result = this.manager.AddCustomerFeedBack(feedbackModel);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added FeedBack Successfully !" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add feedback, Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }
        [HttpDelete]
        [Route("RemoveBooks")]
        public IActionResult RemoveBooks(int BookId)
        {
            try
            {
                var result = this.manager.RemoveBookByAdmin(BookId);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Removed Book Successfully !" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to Remove Book, Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }

        [HttpPost]
        [Route("GetFeedback")]
        public IActionResult GetFeedback(int bookid)
        {
            try
            {
                var result = this.manager.GetCustomerFeedBack(bookid);
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "Feedbackertrived", Data = result });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add to wish list, Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }
        [HttpPut]
        [Route("AddImage")]
        public IActionResult AddImage(IFormFile image)
        {
            try
            {
                var result = this.manager.AddImage(image);
                if (!result.Equals("no image added"))
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added Image Successfully !",Data=result });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add Image, Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }
        [HttpPut]
        [Route("UpdateBook")]
        public IActionResult EditBook(BooksModel Bookdetail)
        {
            try
            {
                var result = this.manager.EditBookDetails(Bookdetail);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book updated  Successfully !" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to updated Book , Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }

    }
}
