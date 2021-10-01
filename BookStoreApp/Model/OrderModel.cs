using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string DateOfOrder { get; set; }
        public BooksModel Books { get; set; }
    }
}
