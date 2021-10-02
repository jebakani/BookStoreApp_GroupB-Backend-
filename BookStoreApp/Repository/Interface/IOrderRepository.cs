using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IOrderRepository
    {
        bool PlaceTheOrder(List<CartModel> orderdetails);
    }
}
