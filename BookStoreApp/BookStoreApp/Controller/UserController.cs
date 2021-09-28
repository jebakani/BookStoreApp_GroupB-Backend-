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
    public class UserController : ControllerBase
    {

        private readonly IUserManager manager;

        public UserController(IUserManager manager)
        {
            this.manager = manager;
            
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterModel userDetails)
        {
            try
            {
                int result = this.manager.Register(userDetails);
                if (result == 1)
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

            } }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginModel loginData)
        {
            var result = this.manager.Login(loginData);
            try
            {
                 if(result!=null)
                {
                    return this.Ok(new  { Status = true, Message = "Login Successful !" ,Data=result});

                }
                 else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Incorrect password" });
                }
            }
            catch(Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }
        [HttpPost]
        [Route("forgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = this.manager.ForgetPassword(email);

                if (result.CustomerId >0)
                {

                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<DataResponseModel>() { Status = false, Message = result.message ,Data=result });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result.message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

    }
}
