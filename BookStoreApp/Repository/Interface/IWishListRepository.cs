using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IWishListRepository
    {
        bool AddToWishList(WishListModel wishListModel);
        bool RemoveFromWishList(int wishListId);
       public  List<WishListModel> GetFromWishList(int userId);
    }
}
