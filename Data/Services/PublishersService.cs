using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using My_Books.Data;
using My_Books.Data.Models;
using My_Books.Data.Models.ViewModels;

namespace My_Books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

       
        public List<Publisher> GetAllPublishers(string sortby, string searchString)
        {
            var allPublishers = _context.Publishers.OrderBy(n=>n.Id).ToList();
            if(!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "name_desc":
                        allPublishers = allPublishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    case "name":
                        allPublishers = allPublishers.OrderBy(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
                
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers.Where(n => n.Name.Contains(searchString, 
                    StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            return allPublishers;
        } 

        //for created status code change
        //public void AddPublisher(PublisherVM publisher)
        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StartsWithNumber(publisher.Name)) throw new PublisherExceptions("Name starts with number", publisher.Name);
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
            return _publisher;
        }
        public Publisher GetPublisherById(int id)=>_context.Publishers.FirstOrDefault(x => x.Id == id);
        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherid)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherid)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();
            return _publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
            if(publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }else

            {
                throw new Exception($"The publisher with id: {id} does not exist");
            }
            
        }

        public bool StartsWithNumber(string name) =>  (Regex.IsMatch(name, @"^\d"));
      
    }
}
