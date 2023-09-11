using Microsoft.AspNetCore.Mvc;
using BooksAPI.Models;
using BooksAPI.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllBooksController : ControllerBase
    {

        private readonly IAuthorInterface _config;
        public AllBooksController(IAuthorInterface configuration)
        {
            _config = configuration;
        }

        // GET: api/<AllBooksController>
        [HttpGet]
        public IEnumerable<GetAllBooksInfo> GetAllBooksInfo()
        {
            return _config.GetAllBooksInfo().ToList();
        }

        /* [HttpGet]
        public IEnumerable<GetAllBooksInfo> GetAllBooksInfo()   //to GET  all books info MEthod
        {
            return _config.GetAllBooksInfo().ToList();
        }*/ 

        // GET api/<AllBooksController>/5
        [HttpGet("{id}")]
        public IEnumerable<GetAllBooksInfo> GetAllBooksInfo(int id) 
        {
            return _config.GetAllBooksInfo(id).ToList();
        }
    

        // POST api/<AllBooksController>
       /* [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AllBooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AllBooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
