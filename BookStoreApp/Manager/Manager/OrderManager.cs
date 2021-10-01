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
        public bool PlaceTheOrder(CartModel orderDetails)
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
