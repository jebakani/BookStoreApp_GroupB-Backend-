using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Inteface
{
    public interface IBookManager
    {
        public List<BooksModel> GetAllBooks();
        bool AddBook(BooksModel bookDetails);
    }
}
