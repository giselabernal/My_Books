using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Books.Data.Models.ViewModels;
using My_Books.Data.Services;
using My_Books.Data.Models;
using System.Collections.Generic;

namespace My_Books.Controllers
{
    [Route("api/Authours")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorsService _authorsService;
        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        //testing while coding if it works depending on the called classes

        [HttpPost("add-author")]
        public ActionResult<Author> Addauthor([FromBody] AuthorVM auth)
        {
            _authorsService.AddAuthor(auth);
            return Ok(); 
        }

       /* public ActionResult<Author> AddAuthor([FromBody] AuthorVM author)
        { 

        }*/

        //testing
        //public AcceptedAtActionResult aceptado([FromHeader] )

        [HttpGet("get-authors-with-books-by-id/{id}")]
        //public ActionResult<List<AuthorVM>> GetAuthorWithBooks(int id)
        public IActionResult GetAuthorWithBooks(int id)
        {
            var response = _authorsService.GetAuthorWithBooks(id);
            return Ok(response);
        }

     
    }
}
