﻿using Manager.Inteface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartManager manager;
        public CartController(ICartManager manager)
        {
            this.manager = manager;

        }
        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddToCart([FromBody] CartModel details)
        {
            try
            {
                var result = this.manager.AddToCart(details);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book is added to cart" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Adding to bag failed ! try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }
        [HttpDelete]
        [Route("RemoveFromCart")]
        public IActionResult RemoveFromCart(int cartId)
        {
            try
            {
                var result = this.manager.DeleteFromCart(cartId);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Removed from cart" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = " failed ! try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }
        [HttpGet]
        [Route("GetCartItem")]
        public IActionResult GetCartItem(int userId)
        {
            try
            {
                var result = this.manager.GetCartItems(userId);
                if (result.Count>0)
                {

                    return this.Ok(new { Status = true, Message = "Removed from cart",Data=result });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No Item in cart" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("UpadetOrderCount")]
        public IActionResult UpadetOrderCount(CartModel cartDetail)
        {
            try
            {
                var result = this.manager.UpdateOrderCount(cartDetail);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "count updated"});
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed TryAgain" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
