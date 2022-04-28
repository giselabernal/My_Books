using System.Collections.Generic;

namespace My_Books.Data.Models.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; }
    }

    //the endpoint will get the publisher and for this publisher 
    //will get all the books that the publisher has published
    public class PublisherWithBooksAndAuthorsVM
    {
        public string Name { get; set; }
        //List of bookauthors list
        public List<BookAuthorVM> BookAuthors { get; set; }
    }
    public class BookAuthorVM
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }
    }
}
