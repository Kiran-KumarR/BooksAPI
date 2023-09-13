using BooksAPI.Interface;
using BooksAPI.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace BooksAPI.Services
{
    public class BooksInfoService:IBooksInfoService
      
    {
        private readonly IConfiguration _configuration;
        private readonly IBooksDatabaseService _databaseService;
      

        public BooksInfoService(IConfiguration configuration, IBooksDatabaseService databaseService)
        {
            _configuration = configuration;
            _databaseService = databaseService;
        }
      
        public async Task<List<BookInfoModel>> FetchBooksFromApiAsync()
        {
            var httpClient = new HttpClient();
            //var apiUrl = "https://www.googleapis.com/books/v1/volumes?q=kaplan%20test%20prep";
           var apiUrl = "https://www.bing.com/books/v1/volumes?q=kaplan%20test%20prep";


            try
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<GoogleBooksApiResponse>(content);

                    if (responseObject.items != null)
                    {
                        var bookInfos = new List<BookInfoModel>();

                        foreach (var item in responseObject.items)
                        {
                            bookInfos.Add(new BookInfoModel
                            {
                                //Id = item.id,
                                title = item.volumeInfo.title,
                                author_name = item.volumeInfo.authors != null ? string.Join(", ", item.volumeInfo.authors) : "No author",
                                publisher_name = item.volumeInfo.publisher,
                                description = item.volumeInfo.description,
                                language = item.volumeInfo.language,
                                maturityRating = item.volumeInfo.maturityRating,
                                pageCount = item.volumeInfo.pageCount,
                                // categories = item.volumeInfo.categories,
                                publishedDate = item.volumeInfo.publishedDate,
                                retailPrice = item.volumeInfo.retailPrice,

                            });
                        }

                        return bookInfos;
                    }
                }
                else
                {
                    Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                    var jsonFile = @"C:\Users\KKumarR\Desktop\BooksAPI\BooksAPI\Database\kaplan_book.json";
                    await RetrieveBooksFromJson(jsonFile);
                    // check comment
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API request failed with exception: {ex.Message}");
            }

            return new List<BookInfoModel>();
        }


        public async Task<List<BookInfoModel>> RetrieveBooksFromJson(string jsonFilePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(jsonFilePath))
                {
                    var jsonString = reader.ReadToEnd();
                    var bookInfos = JsonConvert.DeserializeObject<List<GoogleBooksApiResponse>>(jsonString);
                    var bookInfo = new List<BookInfoModel>();

                    foreach (var item in bookInfos[0].items)
                    {
                        bookInfo.Add(new BookInfoModel
                        {
                            //Id = item.id,
                            title = item.volumeInfo.title,
                            author_name = item.volumeInfo.authors != null ? string.Join(", ", item.volumeInfo.authors) : "No author",
                            publisher_name = item.volumeInfo.publisher,
                            description = item.volumeInfo.description,
                            language = item.volumeInfo.language,
                            maturityRating = item.volumeInfo.maturityRating,
                            pageCount = item.volumeInfo.pageCount,
                            // categories = item.volumeInfo.categories,
                            publishedDate = item.volumeInfo.publishedDate,
                            retailPrice = item.volumeInfo.retailPrice,
                        });
                    }
                    return bookInfo;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RetrieveBooksFromJson: {ex.Message}");
            }

            return new List<BookInfoModel>();
        }

        public async Task StoreBooksInDatabase(List<BookInfoModel> bookInfos)
        {
            string connectionString = _configuration.GetConnectionString("dbconn");
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();

                foreach (var bookInfo in bookInfos)
                {
                    int auth_id = GetOrCreateAuthorId(sqlConnection, bookInfo.author_name);
                    int pub_id = GetOrCreatePublisherId(sqlConnection, bookInfo.publisher_name);
                    int bookId = GetOrCreateBookId(sqlConnection);

                    string insertBookSql = "INSERT INTO Books (id, title, author_id, publisher_id, description,language,maturityRating,pageCount,publishedDate,retailPrice) VALUES (@BookId, @Title, @AuthorId, @PublisherId, LEFT(@Description, 1000),@language,@maturityRating,@pageCount,@publishedDate,@retailPrice)";
                    SqlCommand insertBookCommand = new SqlCommand(insertBookSql, sqlConnection);
                    insertBookCommand.Parameters.AddWithValue("@BookId", bookId);
                    insertBookCommand.Parameters.AddWithValue("@Title", bookInfo.title);
                    insertBookCommand.Parameters.AddWithValue("@AuthorId", auth_id);
                    insertBookCommand.Parameters.AddWithValue("@PublisherId", pub_id);
                    insertBookCommand.Parameters.AddWithValue("@Description", bookInfo.description);
                    insertBookCommand.Parameters.AddWithValue("@language", bookInfo.language);
                    insertBookCommand.Parameters.AddWithValue("@maturityRating", bookInfo.maturityRating);
                    insertBookCommand.Parameters.AddWithValue("@pageCount", bookInfo.pageCount);
                    insertBookCommand.Parameters.AddWithValue("@publishedDate", bookInfo.publishedDate);
                    insertBookCommand.Parameters.AddWithValue("@retailPrice", bookInfo.retailPrice);

                    insertBookCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in StoreBooksInDatabase: {ex.Message}");
                throw; // Re-throw the exception to handle it at a higher level.
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close(); // Ensure the connection is closed, even in case of exceptions.
                }
            }
        }

        public int GetOrCreateAuthorId(SqlConnection connection, string author_name)
        {
            string selectAuthorSql = "SELECT auth_id FROM Author WHERE author_name = @AuthorName";
            SqlCommand authorCommand = new SqlCommand(selectAuthorSql, connection);
            authorCommand.Parameters.AddWithValue("@AuthorName", author_name);

            object authorIdResult = authorCommand.ExecuteScalar();

            if (authorIdResult != null)
            {
                return (int)authorIdResult;
            }
            else
            {

                string insertAuthorSql = "INSERT INTO Author (author_name) VALUES (@AuthorName); SELECT SCOPE_IDENTITY();";
                SqlCommand insertAuthorCommand = new SqlCommand(insertAuthorSql, connection);
                insertAuthorCommand.Parameters.AddWithValue("@AuthorName", author_name);

                return Convert.ToInt32(insertAuthorCommand.ExecuteScalar());
            }
        }

      /*  Task<int> _booksInfoService.GetOrCreateAuthorId(SqlConnection connection, string author_name)
        {
            throw new NotImplementedException();
        }*/


        public int GetOrCreatePublisherId(SqlConnection connection, string publisher_name)
        {
            string selectPublisherSql = "SELECT pub_id FROM Publisher WHERE publisher_name = @PublisherName";
            SqlCommand publisherCommand = new SqlCommand(selectPublisherSql, connection);
            publisherCommand.Parameters.AddWithValue("@PublisherName", publisher_name);


            object publisherIdResult = publisherCommand.ExecuteScalar();

            if (publisherIdResult != null)
            {
                return (int)publisherIdResult;
            }
            else
            {

                string insertPublisherSql = "INSERT INTO Publisher (publisher_name) VALUES (@PublisherName); SELECT SCOPE_IDENTITY();";
                SqlCommand insertPublisherCommand = new SqlCommand(insertPublisherSql, connection);
                insertPublisherCommand.Parameters.AddWithValue("@PublisherName", publisher_name);

                return Convert.ToInt32(insertPublisherCommand.ExecuteScalar());
            }
        }
        /*
        Task<int> IBooksInfoService.GetOrCreatePublisherId(SqlConnection connection, string publisher_name)
        {
            throw new NotImplementedException();
        }*/

        public int GetOrCreateBookId(SqlConnection connection)
        {


            string selectMaxBookIdSql = "SELECT MAX(id) FROM Books";
            SqlCommand selectMaxBookIdCommand = new SqlCommand(selectMaxBookIdSql, connection);
            connection.Open();
            var maxId = selectMaxBookIdCommand.ExecuteScalar();
            if (maxId == DBNull.Value)
            {
                return 1;
            }
            else
            {
                return (int)maxId + 1;
            }

        }

        public int GetBookId(SqlConnection connection)
        {
            string selectMaxBookIdSql = "SELECT MAX(id) FROM Books";
            SqlCommand selectMaxBookIdCommand = new SqlCommand(selectMaxBookIdSql, connection);
            connection.Open();
            var maxId = selectMaxBookIdCommand.ExecuteScalar();
            if (maxId == DBNull.Value)
            {
                return 1;
            }
            else
            {
                return (int)maxId;
            }
        }
        public BookInfoModel GetBookById(int id)
        {
            return _databaseService.RetrieveBookByIdFromDatabase(id);
        }

        public BookInfoModel DeleteBookById(int id)
        {
            return _databaseService.RetrieveBookByIdFromDatabase(id);
        }

        public List<BookInfoModel> PostBooks(BookInfoModel bookInfo)
        {
            string connectionString = _configuration.GetConnectionString("dbconn");
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            List<BookInfoModel> list = new List<BookInfoModel>();

          
            int bookId = GetOrCreateBookId(sqlConnection);
            int authId = GetOrCreateAuthorId(sqlConnection, bookInfo.author_name);
            int pubId = GetOrCreatePublisherId(sqlConnection, bookInfo.publisher_name);

            SqlCommand insertBookCommand = new SqlCommand("INSERT INTO Books(id, title, author_id, publisher_id, description, language, maturityRating, pageCount, publishedDate, retailPrice) " +
                "VALUES (@BookId, @Title, @Author_ID, @Publisher_ID, @Description, @Language, @MaturityRating, @PageCount, @PublishedDate, @RetailPrice);", sqlConnection);

            insertBookCommand.Parameters.AddWithValue("@BookId", bookId);
            insertBookCommand.Parameters.AddWithValue("@Title", bookInfo.title);
            insertBookCommand.Parameters.AddWithValue("@Author_ID", authId);
            insertBookCommand.Parameters.AddWithValue("@Publisher_ID", pubId);
            insertBookCommand.Parameters.AddWithValue("@Description", bookInfo.description);
            insertBookCommand.Parameters.AddWithValue("@Language", bookInfo.language);
            insertBookCommand.Parameters.AddWithValue("@MaturityRating", bookInfo.maturityRating);
            insertBookCommand.Parameters.AddWithValue("@PageCount", bookInfo.pageCount);
            insertBookCommand.Parameters.AddWithValue("@PublishedDate", bookInfo.publishedDate);
            insertBookCommand.Parameters.AddWithValue("@RetailPrice", bookInfo.retailPrice);

            
            insertBookCommand.ExecuteNonQuery();
            sqlConnection.Close();

            // Since you just inserted a book, you can directly add it to the list
            list.Add(new BookInfoModel
            {
                id = bookId,
                title = bookInfo.title,
                auth_id = authId,
                pub_id = pubId,
                description = bookInfo.description,
                language = bookInfo.language,
                maturityRating = bookInfo.maturityRating,
                publishedDate = bookInfo.publishedDate,
                retailPrice = bookInfo.retailPrice
            });

            return list;
        }


        public List<BookInfoModel> PutintoBooks(BookInfoModel bookInfo)
        {
            string connectionString = _configuration.GetConnectionString("dbconn");
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            List<BookInfoModel> list = new List<BookInfoModel>();

            int bookId = GetBookId(sqlConnection);
            int authId = GetOrCreateAuthorId(sqlConnection, bookInfo.author_name);
            int pubId = GetOrCreatePublisherId(sqlConnection, bookInfo.publisher_name);

            SqlCommand sqlCommand = new SqlCommand("UPDATE Books SET " +
        "title = ISNULL(@Title, title), " +
        "author_id = ISNULL(@Author_ID, author_id), " +
        "publisher_id = ISNULL(@Publisher_ID, publisher_id), " +
        "description = ISNULL(@Description, description), " +
        "language = ISNULL(@Language, language), " +
        "maturityRating = ISNULL(@MaturityRating, maturityRating), " +
        "pageCount = ISNULL(@PageCount, pageCount), " +
        "publishedDate = ISNULL(@PublishedDate, publishedDate), " +
        "retailPrice = ISNULL(@RetailPrice, retailPrice) " +
        "WHERE id = @Id;", sqlConnection);  //SELECT * FROM Author WHERE auth_id = @id

            sqlCommand.Parameters.AddWithValue("@Id", bookId);
            sqlCommand.Parameters.AddWithValue("@Title", bookInfo.title);
            sqlCommand.Parameters.AddWithValue("@Author_ID", authId);
            sqlCommand.Parameters.AddWithValue("@Publisher_ID", pubId);
            sqlCommand.Parameters.AddWithValue("@Description", bookInfo.description);

            sqlCommand.Parameters.AddWithValue("@Language", bookInfo.language);
            sqlCommand.Parameters.AddWithValue("@MaturityRating", bookInfo.maturityRating);


            sqlCommand.Parameters.AddWithValue("@PageCount", bookInfo.pageCount);
            sqlCommand.Parameters.AddWithValue("@PublishedDate", bookInfo.publishedDate);
            sqlCommand.Parameters.AddWithValue("@RetailPrice", bookInfo.retailPrice);


            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            list.Add(new BookInfoModel
            {
                id = bookId,
                title = bookInfo.title,
                auth_id = authId,
                pub_id = pubId,
                description = bookInfo.description,
                language = bookInfo.language,
                maturityRating = bookInfo.maturityRating,
                publishedDate = bookInfo.publishedDate,
                retailPrice = bookInfo.retailPrice
            });

            return list;

        }
    }
}
