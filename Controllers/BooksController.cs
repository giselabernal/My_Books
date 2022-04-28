using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Books.Data.Models.ViewModels;
using My_Books.Data.Services;
using System;
using System.Collections.Generic;
using My_Books.Data.Models;
using System.Threading.Tasks;

namespace My_Books.Controllers
{
   // [ApiController]
  //  [Route("api/Books")]
   
    public class BooksController : ControllerBase
    {
        public BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }
        [HttpGet("get-all-books")]
        //testing followin line of code
        /*    public ActionResult<IEnumerable<Book>> getallbooks()
            {
                var allBooks = _booksService.GetAllBooksi();
                return Ok(new { userid = 101, username = "Prueba" });
            }*/
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllBooks()
        {
            var allBooks = _booksService.GetAllBooks();

            return Ok(allBooks);
        }
        [HttpGet("get-book-byid/{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _booksService.GetBookById(id);
            return Ok(book);
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            //comentando esto para meterlo al try
           // string mensaje= _booksService.AddBookWithAuthors(book);
            //if (mensaje == string.Empty)
            //    return Ok();
            //else
            //    return EmptyResult();
            //hasta que llegue al try catch vi como

            try
            {
                _booksService.AddBookWithAuthors(book);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

       

        private IActionResult EmptyResult()
        {
            return new JsonResult(new { message = "Unable to add data since Publisher Id does not exist" });
        }
        //missing //dudas
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
      /*  public async Task<IActionResult> CreatesAsync(int id)
        {
            await 
        }*/

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookVM book)
        {
            var updatedBook = _booksService.UpdateBookById(id, book);   
            return Ok(updatedBook);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            _booksService.DeleteBookById(id);
            return Ok();
        }
    }
}
