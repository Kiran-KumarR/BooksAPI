using Microsoft.AspNetCore.Mvc;
using BooksAPI.Controllers;
using BooksAPI.Models;
using BooksAPI.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksInfoController : ControllerBase
    {

        private readonly IAuthorInterface _config;
        public BooksInfoController(IAuthorInterface configuration)
        {
            _config = configuration;
        }


        // GET: api/<BooksInfoController>
        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] bool seed = true)//seed=faalse
        {
            if (seed)
            {
                var booksFromDatabase = _config.RetrieveBooksFromDatabase();

                if (booksFromDatabase.Count > 0)
                {
                    return Ok(booksFromDatabase);
                }
                else
                {
                    return NotFound("No books found in the database.");
                }
            }
            else
            {
                var booksFromApi = await _config.FetchBooksFromApiAsync();
                //     RetrieveBooksFromDatabase(booksFromApi);

                if (booksFromApi.Count > 0)
                {
                    // await StoreBooksInDatabase(booksFromApi);
                    List<BookInfoModel> bookInfos = booksFromApi;
                    await _config.StoreBooksInDatabase(bookInfos);
                    return Ok(booksFromApi);
                }
                else
                {
                    return NotFound("No books found from the API.");
                }
            }
        }

        // GET api/<BooksInfoController>/5
        [HttpGet("{id}")]
        public IEnumerable<BookInfoModel> GetBooks(int id)   //to GET MEthod
        {
            return _config.GetBooks(id).ToList();
        }

        // POST api/<BooksInfoController>
        [HttpPost]
        public List<BookInfoModel> PostBooks()
        {
            return _config.PostBooks().ToList();
        }

        // PUT api/<BooksInfoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksInfoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {


        }



    }
}
