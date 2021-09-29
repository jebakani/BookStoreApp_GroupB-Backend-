using Manager.Inteface;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controller
{
    public class WishListController : ControllerBase
    {
        private readonly IWishListManager manager;

        public WishListController(IWishListManager manager)
        {
            this.manager = manager;

        }
        [HttpPost]
        [Route("RemoveFromWishList")]
        public IActionResult Register([FromBody] WishListModel wishListModel)
        {
            try
            {
                var result = this.manager.AddToWishList(wishListModel);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added To wish list Successfully !" });
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
        [HttpDelete]
        [Route("AddToWishList")]
        public IActionResult RemoveFromWishList(int wishListId)
        {
            try
            {
                var result = this.manager.RemoveFromWishList(wishListId);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Removed from Wish list Successfully !" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to Remove From wish list, Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }

    }
}
