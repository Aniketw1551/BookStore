using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IBookRL
    {
        public AddBookModel AddBook(AddBookModel addBook);
        public BookModel GetBookByBookId(int bookId);
        public List<BookModel> GetAllBooks();
        public BookModel UpdateBookDetails(BookModel bookModel);
        public bool DeleteBook(int bookId);
    }
}