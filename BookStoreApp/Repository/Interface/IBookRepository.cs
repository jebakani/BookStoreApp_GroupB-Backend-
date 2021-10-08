using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Inteface
{
    public interface IBookRepository
    {
        List<BooksModel> GetAllBooks();
        bool AddBook(BooksModel bookDetails);
        BooksModel GetBookDetail(int bookId);
        bool AddCustomerFeedBack(FeedbackModel feedbackModel);
        List<FeedbackModel> GetCustomerFeedBack(int bookid);
        string AddImage(IFormFile image);
        bool EditBookDetails(BooksModel bookDetails);
        public bool RemoveBookByAdmin(int bookId);
    }
}
