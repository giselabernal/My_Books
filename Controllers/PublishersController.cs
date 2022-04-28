using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Books.Data.Models;
using My_Books.Data.Services;
using My_Books.Data.Models.ViewModels;
using System;
using My_Books.ActionResults;

namespace My_Books.Controllers
{
    [Route("api/Publishers")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _pulishersService;

        public PublishersController(PublishersService pulishersService)
        {
            _pulishersService = pulishersService;
        }

        [HttpGet("Get-All-Publishers")]
        public IActionResult GetAllPublishers(string sortby, string searchString)
        {
            try
            {
                var _result = _pulishersService.GetAllPublishers(sortby, searchString);
                return Ok(_result);
            }
            catch (Exception)
            {

                return BadRequest("We could not load any data");
            }  
           
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            try
            {
                var _response = _pulishersService.GetPublisherData(id);
                return Ok(_response);
            }
            catch (Exception)
            {
                return BadRequest("We could not load any data");
            }
           
        }

        [HttpGet("get-publisher-by-id/{id}")]
        //here is better to use iactionresult cause if its as content type, it says "success"
        //when it is not thruth 
        public CustomActionResult GetPublisherById(int id)
        //public Publisher GetPublisherById(int id)
        {
            //throw new Exception("This is an error exception handled by middleware");
            var _response = _pulishersService.GetPublisherById(id);

            if (_response != null)
            {
                var _responseObj = new CustomActionResultVM()
                {
                    Publisher = _response
                };
                return new CustomActionResult(_responseObj);
                //return _response
               
                //return null;
            }
            else
            {
                var _responseObj = new CustomActionResultVM()
                {
                    Publisher = _response
                };
                return new CustomActionResult(_responseObj);
                //return NotFound();
            }

        }

        [HttpPost("add-publishers")]
        public IActionResult AddPublishers([FromBody]PublisherVM publisher)
        {
            try
            {
                var newpub = _pulishersService.AddPublisher(publisher);
                //return OK();
                return Created(nameof(AddPublishers), newpub);
            }
            catch (PublisherExceptions ex)
            {
                return BadRequest($"{ex.Message}, PublisherName: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
        }

        
        [HttpDelete("Remove-publisher-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult DeletePublisherById(int id)
        {
            try
            {
                _pulishersService.DeletePublisherById(id);
                return Accepted("ok");
            }
            catch (System.Exception)
            {
                return NoContent();
            }
            
        }

       
    }
}
