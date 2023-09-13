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
        public async Task<IActionResult> GetAllBooks([FromQuery] bool seed = true)
        {
            if (seed)
            {
                var booksFromDatabase = _config.RetrieveBooksFromDatabase();
                if (booksFromDatabase.Count == 0)
                {
                    // Database is empty, try fetching from API
                    var booksFromApi = await _config.FetchBooksFromApiAsync();
                    if (booksFromApi != null && booksFromApi.Count > 0)
                    {
                        await _config.StoreBooksInDatabase(booksFromApi);
                        return Ok(booksFromApi);
                    }
                    else
                    {
                        // API failed, try fetching from local JSON file
                        var jsonFile = @"C:\Users\KKumarR\Desktop\BooksAPI\BooksAPI\Database\kaplan_book.json";
                        var booksFromJsonTask = _config.RetrieveBooksFromJson(jsonFile); //obtain the actual list first then await it and then checking its count
                        var booksFromJson = await booksFromJsonTask;

                        if (booksFromJson != null && booksFromJson.Count > 0)
                        {
                            await _config.StoreBooksInDatabase(booksFromJson);
                            return Ok(booksFromJson);
                        }

                        else
                        {
                            return NotFound("No books found from API or local JSON file.");
                        }
                    }
                }
                else
                {
                    // Database is not empty, return records from the database
                    return Ok(booksFromDatabase);
                }
            }
            else
            {
                // Seed is false, check if the database is empty
                var booksFromDatabase = _config.RetrieveBooksFromDatabase();
                if (booksFromDatabase.Count == 0)
                {
                    return NotFound("No records found in the database.");
                }
                else
                {
                    // Database is not empty, return records from the database
                    return Ok(booksFromDatabase);
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
        public List<BookInfoModel> PutintoBooks(int id, string title)
        {
            return _config.PutintoBooks(id, title).ToList();
        }

        // DELETE api/<BooksInfoController>/5
        [HttpDelete("{id}")]
        public List<BookInfoModel> DeleteBook(int id)
        {
            return _config.DeleteBook(id).ToList();

        }



    }
}
