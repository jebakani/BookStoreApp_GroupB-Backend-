using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Inteface
{
    public interface IUserManager
    { 
        public int Login(RegisterModel userDetails);
    }
}
