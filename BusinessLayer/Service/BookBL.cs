using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;

        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public AddBookModel AddBook(AddBookModel addBook)
        {
            try
            {
                return this.bookRL.AddBook(addBook);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BookModel GetBookByBookId(int bookId)
        {
            try
            {
                return this.bookRL.GetBookByBookId(bookId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BookModel UpdateBookDetails(BookModel bookModel)
        {
            try
            {
                return this.bookRL.UpdateBookDetails(bookModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteBook(int bookId)
        {
            try
            {
                return this.bookRL.DeleteBook(bookId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
