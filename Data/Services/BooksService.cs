using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using My_Books.Data;
using My_Books.Data.Models;
using My_Books.Data.Models.ViewModels;

namespace My_Books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context; 
        }
        //dejando el try catch desde el controller se quita la sig linea y el try
        //public string AddBookWithAuthors(BookVM book)
        public void AddBookWithAuthors(BookVM book)
        {
            //string msg = "";
            //try
            //{
                var _book = new Book()
                {
                    Title = book.Title,
                    Description = book.Description,
                    IsRead = book.IsRead,
                    DateRead = book.IsRead ? book.DateRead.Value : null,
                    Rate = book.IsRead ? book.Rate.Value : null,
                    Genre = book.Genre,
                    //Author = book.Author,
                    CoverUrl = book.CoverUrl,
                    DateAdded = DateTime.Now,
                    PublisherId = book.PublisherId
                };
                _context.Books.Add(_book);
                _context.SaveChanges();
                foreach (var id in book.AuthorsId)
                {
                    var _book_author = new Book_Author()
                    {
                        BookId = _book.Id,
                        AuthorId = id
                    };
                    _context.Books_Authors.Add(_book_author);
                    _context.SaveChanges();
                }

                /*   }
                   catch (Exception ex)
                   {
                       msg = ex.Message;
                   }
                   return msg;*/

            }

                //wrong   var bk = _context.Books.Select(n=> _book.Id).FirstOrDefault();

            /*  public List<Book> GetAllBooks()
              {
                  return _context.Books.ToList();
              }*/

            //if I want it even more shorter than above
            public List<Book> GetAllBooks() => _context.Books.ToList();

        //testing following line
       // public IEnumerable<Book> GetAllBooksi() => _context.Books;

        //modified after booksauthor relationship
       // public Book GetBookById(int bookId) => _context.Books.FirstOrDefault(n=>n.Id == bookId);

        //following is the good one with the relationship bookswithauthor
        public BookWithAutorsVM GetBookById(int bookId)
        {
            var _bookWithAuthors = _context.Books.Where(n => n.Id == bookId).Select(book => new BookWithAutorsVM()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                //Author = book.Author,
                CoverUrl = book.CoverUrl,
                //DateAdded = DateTime.Now,
                PublisherName = book.Publisher.Name,
                AuthorsNames= book.Book_Authors.Select(n=>n.Author.FullName).ToList()

            }).FirstOrDefault() ;
            return _bookWithAuthors;
        }

        public Book UpdateBookById(int bookId, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(n=>n.Id == bookId);
            if (book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                //_book.Author = book.Author;
                _book.CoverUrl = book.CoverUrl;
                _book.DateAdded = DateTime.Now;
                _book.PublisherId = book.PublisherId;
              //  _context.Books.Add(_book);
                _context.SaveChanges();

            
            }
            return _book;
        }

        public void DeleteBookById(int bookId)
        {
            var _book= _context.Books.FirstOrDefault(n=>n.Id == bookId);
            if( _book != null )
            {
                _context.Books.Remove( _book );
                _context.SaveChanges();
            }
        }
    }
}
