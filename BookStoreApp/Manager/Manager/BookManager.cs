﻿using Manager.Inteface;
using Microsoft.AspNetCore.Http;
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
        public BooksModel GetBookDetail(int bookId)
        {
            try
            {
                return this.repository.GetBookDetail(bookId);


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool AddCustomerFeedBack(FeedbackModel feedbackModel)
        {
            try
            {
                return this.repository.AddCustomerFeedBack(feedbackModel);


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<FeedbackModel> GetCustomerFeedBack(int bookid)
        {

            try
            {
                return this.repository.GetCustomerFeedBack(bookid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       public string AddImage(IFormFile image)
        {
            try
            {
                return this.repository.AddImage(image);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       public bool EditBookDetails(BooksModel bookDetails)
        {
            try
            {
                return this.repository.EditBookDetails(bookDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveBookByAdmin(int bookId)
        {
            try
            {
                return this.repository.RemoveBookByAdmin(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
