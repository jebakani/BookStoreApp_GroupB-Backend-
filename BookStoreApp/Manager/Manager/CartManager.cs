using Manager.Inteface;
using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepository repository;
        public CartManager(ICartRepository repository)
        {
            this.repository = repository;
        }
        public bool AddToCart(CartModel details)
        {
            try
            {
                return this.repository.AddToCart(details);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       public bool DeleteFromCart(int cartId)
        {
            try
            {
                return this.repository.DeleteFromCart(cartId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CartModel> GetCartItems(int userId)
        { try
            {
                return this.repository.GetCartItems(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateOrderCount(CartModel cartDetail)
        {
            try
            {
                return this.repository.UpdateOrderCount(cartDetail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
