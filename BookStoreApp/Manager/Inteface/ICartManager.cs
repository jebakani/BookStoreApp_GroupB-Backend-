using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Inteface
{
    public interface ICartManager
    {
        bool DeleteFromCart(int cartId);
        bool AddToCart(CartModel details);
        List<CartModel> GetCartItems(int userId);
        bool UpdateOrderCount(CartModel cartDetail);
    }
}
