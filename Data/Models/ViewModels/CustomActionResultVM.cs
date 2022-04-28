using System;
using My_Books.Data.Models;

namespace My_Books.Data.Models.ViewModels
{
    public class CustomActionResultVM
    {
        public Exception Exception { get; set; }
        public Publisher Publisher { get; set; }    
    }
}
