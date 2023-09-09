using BooksAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        ContextDb db=new ContextDb();
        // GET: api/<AuthorController>
        [HttpGet]
        public IEnumerable<AuthorModel> Get()   //to GET MEthod
        {
            return db.Get().ToList();
        }


        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public IEnumerable<AuthorModel> Get(int id)   //to GET MEthod
        {
            return db.Get(id).ToList();
        }

        // POST api/<AuthorController>
        [HttpPost]
        public IEnumerable<AuthorModel> Post()   //to Post MEthod
        {
            return db.Post().ToList();
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public IEnumerable<AuthorModel> Put(int id,string name)   //to Post MEthod
        {
            return db.Put(id,name).ToList();
        }


        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
