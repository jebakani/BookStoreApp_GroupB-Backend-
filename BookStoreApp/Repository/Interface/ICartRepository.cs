using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
   public interface ICartRepository
    {
        bool AddToCart(CartModel details);
        bool DeleteFromCart(int cartId);
    }
}
