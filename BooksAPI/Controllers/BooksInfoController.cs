using Microsoft.AspNetCore.Mvc;
using BooksAPI.Controllers;
using BooksAPI.Models;
using BooksAPI.Interface;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksInfoController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IBooksInfoService _bookService;
        private readonly IBooksDatabaseService _databaseService;

        public BooksInfoController(IConfiguration configuration, IBooksInfoService bookService, IBooksDatabaseService databaseService)
        {
            _configuration = configuration;
            _bookService = bookService;
            _databaseService = databaseService;
        }



        // GET: api/<BooksInfoController>
        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] bool seed = true)
        {
            if (seed)
            {
                var booksFromDatabase = _databaseService.RetrieveBooksFromDatabase();
                if (booksFromDatabase.Count == 0)
                {
                    // Database is empty, try fetching from API
                    var booksFromApi = await _bookService.FetchBooksFromApiAsync();
                    if (booksFromApi != null && booksFromApi.Count > 0)
                    {
                        await _bookService.StoreBooksInDatabase(booksFromApi);
                        return Ok(booksFromApi);
                    }
                    else
                    {
                        // API failed, try fetching from local JSON file
                        var jsonFile = @"C:\Users\KKumarR\Desktop\BooksAPI\BooksAPI\Database\kaplan_book.json";
                        var booksFromJsonTask = _bookService.RetrieveBooksFromJson(jsonFile); //obtain the actual list first then await it and then checking its count
                        var booksFromJson = await booksFromJsonTask;

                        if (booksFromJson != null && booksFromJson.Count > 0)
                        {
                            await _bookService.StoreBooksInDatabase(booksFromJson);
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
                var booksFromDatabase = _databaseService.RetrieveBooksFromDatabase();
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
        public IActionResult GetBooks(int id)   //to GET MEthod
        {

            var book = _bookService.GetBookById(id);    

            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound($"Book with ID {id} not found.");
            }
        }

        // POST api/<BooksInfoController>
      
        [HttpPost]
        public List<BookInfoModel> PostBooks(BookInfoModel bookInfo)
        {
           

            return _bookService.PostBooks( bookInfo).ToList();
        }

        // PUT api/<BooksInfoController>/5
        [HttpPut("{id}")]
        public List<BookInfoModel> PutintoBooks(BookInfoModel bookInfo)
        {
            return _bookService.PutintoBooks(bookInfo).ToList();
        }



        // DELETE api/<BooksInfoController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            bool deletionResult = _databaseService.DeleteBookFromDatabase(id);//change 

            if (deletionResult)
            {
                return Ok($"Book with ID {id} has been deleted.");

            }
            else
            {
                return NotFound($"Book with ID {id} not found.");
            }

        }



    }
}
