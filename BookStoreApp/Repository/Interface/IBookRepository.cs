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
    }
}
