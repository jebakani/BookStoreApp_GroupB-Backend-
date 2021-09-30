using Manager.Inteface;
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
        [Route("Add")]
        public IActionResult AddBook([FromBody] BooksModel bookDetails)
        {
            try
            {
                var result = this.manager.AddBook(bookDetails);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added New User Successfully !" });
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
    }
}
