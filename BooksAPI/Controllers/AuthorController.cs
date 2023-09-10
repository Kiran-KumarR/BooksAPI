using BooksAPI.Interface;
using BooksAPI.Models;
using Microsoft.AspNetCore.Mvc;
using BooksAPI.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorInterface _config;
        public AuthorController(IAuthorInterface configuration)
        {
            _config = configuration;
        }
       //ContextDb db=new ContextDb();


        // GET: api/<AuthorController>
        [HttpGet]
        public IEnumerable<AuthorModel> Get()   //to GET MEthod
        {
            return _config.Get().ToList();
        }


        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public IEnumerable<AuthorModel> Get(int id)   //to GET MEthod
        {
            return _config.Get(id).ToList();
        }

        // POST api/<AuthorController>
        [HttpPost]
        public IEnumerable<AuthorModel> Post()   //to Post MEthod
        {
            return _config.Post().ToList();
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public IEnumerable<AuthorModel> Put(int id,string name)   //to Post MEthod
        {
            return _config.Put(id,name).ToList();
        }


        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public IEnumerable<AuthorModel> Delete(int id)   //to Post MEthod
        {
            return _config.Delete(id).ToList();
        }
    }
}
