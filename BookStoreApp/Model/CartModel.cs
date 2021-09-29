using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model
{
    public class CartModel
    {
        public int CartID { get; set; }
        public int UserId { get; set; }
        public int  BookID { get; set; }
        [DefaultValue(1)]
        public int BookOrderCount { get; set; }
        public BooksModel Books { get; set; }
    }
}
