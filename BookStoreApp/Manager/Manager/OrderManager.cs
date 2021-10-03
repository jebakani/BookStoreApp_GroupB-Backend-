using Manager.Inteface;
using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class OrderManager:IOrderManager
    {
        private readonly IOrderRepository repository;
        public OrderManager(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public List<OrderModel> GetOrderList(int userId)
        {
            try
            {
                return this.repository.GetOrderList(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PlaceTheOrder(List<CartModel> orderDetails)
        {
            try
            {
                return this.repository.PlaceTheOrder(orderDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
