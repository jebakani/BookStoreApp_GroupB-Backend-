﻿using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Inteface
{
    public interface IOrderManager
    {
        bool PlaceTheOrder(List<CartModel> orderdetails);
         List<OrderModel> GetOrderList(int userId);
    }
}
