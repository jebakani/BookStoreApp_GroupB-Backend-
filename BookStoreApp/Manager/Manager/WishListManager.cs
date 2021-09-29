using Manager.Inteface;
using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class WishListManager : IWishListManager
    {
        private readonly IWishListRepository repository;
        public WishListManager(IWishListRepository repository)
        {
            this.repository = repository;
        }

        public bool AddToWishList(WishListModel wishListModel)
        {
            try
            {
                return this.repository.AddToWishList(wishListModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
