using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using My_Books.Data;
using My_Books.Data.Models;
using My_Books.Data.Models.ViewModels;

namespace My_Books.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;

        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();
        }

        public AuthorWithBooksVM GetAuthorWithBooks(int AuthorId)
        {
            var author = _context.Authors.Where(n => n.Id == AuthorId).Select(n => new AuthorWithBooksVM()
            {
                FullName = n.FullName,
                BookTitles = n.Book_Authors.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();
            return author;
        }
    }
}
