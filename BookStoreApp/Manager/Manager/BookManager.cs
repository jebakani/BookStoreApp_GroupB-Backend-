using Manager.Inteface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{

    public class BookManager :IBookManager

    {
        private readonly IBookRepository repository;
        public BookManager(IBookRepository repository)
        {
            this.repository = repository;
        }

        public List<BooksModel> GetAllBooks()
        {
            try
            {
                return this.repository.GetAllBooks();


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool AddBook(BooksModel bookDetails)
        {
            try
            {
                return this.repository.AddBook(bookDetails);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
