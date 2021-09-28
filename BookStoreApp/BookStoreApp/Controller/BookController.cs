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
                if (result.Count>0)
                {
                    return this.Ok(new { Status = true, Message = "All Notes", data=result });

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
