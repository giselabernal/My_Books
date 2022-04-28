using System;
namespace My_Books
{
    [Serializable]
    public class PublisherExceptions : Exception
    {
        public string PublisherName { get; set; }

        public PublisherExceptions()
        {

        }

        public PublisherExceptions(string message) :base(message)
        {

        }

        public PublisherExceptions(string message, Exception inner): base(message, inner)
        {

        }

        public PublisherExceptions(string message, string publisherName) :this(message)
        {
            PublisherName = publisherName;
        }
    }
}
