using System.Data.SqlClient;
using System.Data;
//using System.Configuration;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BooksAPI.Interface;
using System.Configuration;
using Newtonsoft.Json;

namespace BooksAPI.Models
{
    public class ContextDb:IAuthorInterface
    {
       

        public string dbconn = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = BooksAPI_Db; Integrated Security = True";
         SqlConnection sqlConnection;

         // public SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
         public ContextDb()
         {
             sqlConnection = new SqlConnection(dbconn);
         }
        //string connectionString = _configuration.GetConnectionString("DefaultConnection");
        //SqlConnection sqlConnection = new SqlConnection(connectionString);
       // private readonly IConfiguration _configuration;

        /*public ContextDb(IConfiguration configuration)
        {
            _configuration = configuration;
        }*/
        /// <summary>
        /// GET
        /// METHOD
        /// 
        /// </summary>
        /// <returns></returns>
        public List<AuthorModel> Get()
        {
            List<AuthorModel> list= new List<AuthorModel>();
            //string sqlConnection = _configuration.GetConnectionString("DefaultConnection");
            SqlCommand sqlCommand = new SqlCommand("select * from Author",sqlConnection);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adp.Fill(dt); //fill the datatable ,no need to use open and close connection by using adapater

            foreach(DataRow dr in dt.Rows)
            {
                list.Add(new AuthorModel
                {
                    auth_id = Convert.ToInt32(dr["auth_id"]),
                    author_name = Convert.ToString(dr["author_name"])
                });
            }
            return list;
            sqlConnection.Close();

        }
        /// <summary>
        /// POST METHOD 
        /// </summary>
        /// <returns></returns>
        public List<AuthorModel> Post() {

            List<AuthorModel> list = new List<AuthorModel>();
            SqlCommand sqlCommand = new SqlCommand("  INSERT INTO Author (author_name) VALUES ('Stephen King'),('MAXWELL'),('Steve Smith'); ", sqlConnection);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adp.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new AuthorModel
                {
                    auth_id = Convert.ToInt32(dr["auth_id"]),
                    author_name = Convert.ToString(dr["author_name"])
                });
            }
            return list;



        }

        /// <summary>
        /// GET METHOD BY ID
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public List<AuthorModel> Get(int id)
        {
            List<AuthorModel> list = new List<AuthorModel>();
            SqlCommand sqlCommand = new SqlCommand("select * from Author where auth_id=@id", sqlConnection);//SELECT * FROM Author WHERE auth_id = @id
            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adp.Fill(dt); //fill the datatable ,no need to use open and close connection by using adapater

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new AuthorModel
                {
                    auth_id = Convert.ToInt32(dr["auth_id"]),
                    author_name = Convert.ToString(dr["author_name"])
                });
            }
            return list;

        }

        /// <summary>
        ///  PUT METHOD BY ID
        /// to replace an existing resource entirely, you can use PUT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<AuthorModel> Put(int id, string name)
        {
            List<AuthorModel> list = new List<AuthorModel>();
            SqlCommand sqlCommand = new SqlCommand("UPDATE Author SET author_name = @name WHERE auth_id = @id", sqlConnection);//SELECT * FROM Author WHERE auth_id = @id
            sqlCommand.Parameters.AddWithValue("@name", name);
            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adp.Fill(dt); //fill the datatable ,no need to use open and close connection by using adapater

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new AuthorModel
                {
                    auth_id = Convert.ToInt32(dr["auth_id"]),
                    author_name = Convert.ToString(dr["author_name"])
                });
            }
            return list;

        }
        /// <summary>
        /// Delete Method 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<AuthorModel> Delete(int id)
        {
            List<AuthorModel> list = new List<AuthorModel>();
            SqlCommand sqlCommand = new SqlCommand("Delete from Author where auth_id=@id", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adp.Fill(dt); //fill the datatable ,no need to use open and close connection by using adapater

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new AuthorModel
                {
                    auth_id = Convert.ToInt32(dr["auth_id"]),
                    author_name = Convert.ToString(dr["author_name"])
                });
            }
            return list;

        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        public List<GetAllBooksInfo> GetAllBooksInfo()
        {
            List<GetAllBooksInfo> list = new List<GetAllBooksInfo>();
            SqlCommand sqlCommand = new SqlCommand("EXEC GetAllBooksInfo", sqlConnection);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adp.Fill(dt); //fill the datatable ,no need to use open and close connection by using adapater

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new GetAllBooksInfo
                {
                    id = Convert.ToInt32(dr["id"]),
                    title = Convert.ToString(dr["title"]),
                    auth_name = Convert.ToString(dr["auth_name"]),
                    publisher_name = Convert.ToString(dr["publisher_name"]),
                    description = Convert.ToString(dr["description"]),
                    language = Convert.ToString(dr["language"]),
                    maturityRating = Convert.ToString(dr["maturityRating"]),
                    pageCount = Convert.ToInt32(dr["pageCount"]),
                    categories = Convert.ToString(dr["categories"]),
                    publishedDate = Convert.ToString(dr["publishedDate"]),
                    retailPrice = Convert.ToDecimal(dr["retailPrice"])


                });
            }
            return list;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public List<GetAllBooksInfo> GetAllBooksInfo(int id)
        {
            List<GetAllBooksInfo> list = new List<GetAllBooksInfo>();
            SqlCommand sqlCommand = new SqlCommand("GetAllBooksInfoById", sqlConnection);
            //Sqlcommand sqlCommand = new SqlCommand("GetFullNameById", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", id);
            //connection.Open();
            //sqlCommand.Parameters.AddWithValue("@id", id);
            sqlConnection.Open();
             var reader = sqlCommand.ExecuteReader();

            
               if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                list.Add(new GetAllBooksInfo
                {
                    id = Convert.ToInt32(reader.GetValue(0)),
                    title = Convert.ToString(reader.GetValue(1)),
                    auth_name = Convert.ToString(reader.GetValue(2)),
                    publisher_name = Convert.ToString(reader.GetValue(3)),
                    description = Convert.ToString(reader.GetValue(4)),
                    language = Convert.ToString(reader.GetValue(5)),
                    maturityRating = Convert.ToString(reader.GetValue(6)),
                    pageCount = Convert.ToInt32(reader.GetValue(7)),
                    categories = Convert.ToString(reader.GetValue(8)),
                    publishedDate = Convert.ToString(reader.GetValue(9)),
                    retailPrice = Convert.ToDecimal(reader.GetValue(10))


                });
            
                  }

            }
            return list;

        }

       


        public List<BooksModel> RetrieveBooksFromDatabase()
        {
            List<BooksModel> books = new List<BooksModel>();

            try
            {
                // string connectionString = GetConnectionString("sqlConnection");


                //SqlCommand sqlCommand = new SqlCommand("GetAllBooksInfoById", sqlConnection);
                sqlConnection.Open();


              string selectSql = " SELECT B.id , B.title,A.auth_id ,   A.author_name ,  P.pub_id  ,  P.publisher_name ,   B.description  ,  B.language , B.maturityRating ,  B.pageCount,   B.publishedDate, B.retailPrice FROM    Books B INNER JOIN Author A ON B.author_id = A.auth_id INNER JOIN Publisher P ON B.publisher_id = P.pub_id ";
                 SqlCommand command = new SqlCommand(selectSql, sqlConnection);

                    SqlDataReader reader = command.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var book = new BooksModel
                            {
                                id=reader.GetInt32(0),
                                title = reader.GetString(1),
                                auth_id=reader.GetInt32(2),
                                author_name = reader.GetString(3),
                                pub_id = reader.GetInt32(4),
                                publisher_name = reader.GetString(5),
                                description = reader.GetString(6),
                                language = reader.GetString(7),
                                maturityRating = reader.GetString(8),
                                pageCount = reader.GetInt32(9),
                               // categories=reader.GetString(10),
                                publishedDate = reader.GetString(10),
                                retailPrice=reader.GetDecimal(11)
                                

                            };

                            books.Add(book);
                        }
                    }


                    //sqlConnection.Close();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return books;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public  async Task<List<BookInfoModel>> FetchBooksFromApiAsync()
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
                                description =item.volumeInfo.description,
                                language=item.volumeInfo.language,
                                maturityRating=item.volumeInfo.maturityRating,
                                pageCount=item.volumeInfo.pageCount,
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
                    using (StreamReader reader = new StreamReader(jsonFile))
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API request failed with exception: {ex.Message}");
            }

            return new List<BookInfoModel>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookInfos"></param>
        /// <returns></returns>

        public async Task StoreBooksInDatabase(List<BookInfoModel> bookInfos)
        {
            SqlConnection sqlConnection = new SqlConnection(dbconn);

            try
            {
                sqlConnection.Open();

                foreach (var bookInfo in bookInfos)
                {
                    int auth_id = GetOrCreateAuthorId(sqlConnection, bookInfo.author_name);
                    int pub_id = GetOrCreatePublisherId(sqlConnection, bookInfo.publisher_name);
                    int bookId = GetUniqueBookId(sqlConnection);

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



        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="author_name"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="publisher_name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public int GetUniqueBookId(SqlConnection connection)
        {
           
               
                string selectMaxBookIdSql = "SELECT MAX(id) FROM Books";
                SqlCommand selectMaxBookIdCommand = new SqlCommand(selectMaxBookIdSql, connection);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<BookInfoModel> GetBooks(int id)
        {
            List<BookInfoModel> list = new List<BookInfoModel>();
            SqlCommand sqlCommand = new SqlCommand("select * from Books where id=@id", sqlConnection);//SELECT * FROM Author WHERE auth_id = @id
            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adp.Fill(dt); //fill the datatable ,no need to use open and close connection by using adapater

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new BookInfoModel
                {
                    id = Convert.ToInt32(dr["id"]),
                    title = Convert.ToString(dr["title"]),
                    auth_id = Convert.ToInt32(dr["author_id"]),
                    pub_id = Convert.ToInt32(dr["publisher_id"]),
                    description = Convert.ToString(dr["description"]),
                    language = Convert.ToString(dr["language"]),
                    maturityRating = Convert.ToString(dr["maturityRating"]),
                    publishedDate= Convert.ToString(dr["publishedDate"]),
                    retailPrice = Convert.ToDecimal(dr["retailPrice"])
                });
            }
            return list;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BookInfoModel> PostBooks()
        {

            List<BookInfoModel> list = new List<BookInfoModel>();

            int bookId = GetUniqueBookId(sqlConnection);

            SqlCommand sqlCommand1 = new SqlCommand(" insert into Books(id,title,author_id,publisher_id,description,language,maturityRating,pageCount,publishedDate,retailPrice) \r\nvalues (@BookId,'HarryPotter',3000,2000,'Fiction Book into the world of harry potter','English','Mature',420,'12-09-2008',88.31); ", sqlConnection);
            SqlCommand insertBookCommand = sqlCommand1;
            insertBookCommand.Parameters.AddWithValue("@BookId", bookId);

            SqlDataAdapter adp = new SqlDataAdapter(insertBookCommand);

            DataTable dt = new DataTable();
            adp.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new BookInfoModel
                {
                    id = Convert.ToInt32(dr["id"]),
                    title = Convert.ToString(dr["title"]),
                    auth_id = Convert.ToInt32(dr["author_id"]),
                    pub_id = Convert.ToInt32(dr["publisher_id"]),
                    description = Convert.ToString(dr["description"]),
                    language = Convert.ToString(dr["language"]),
                    maturityRating = Convert.ToString(dr["maturityRating"]),
                    publishedDate = Convert.ToString(dr["publishedDate"]),
                    retailPrice = Convert.ToDecimal(dr["retailPrice"])
                });
            }
            return list;

        }

        public List<BookInfoModel> PutintoBooks(int id, string title)
        {
            List<BookInfoModel> list = new List<BookInfoModel>();
            SqlCommand sqlCommand = new SqlCommand("UPDATE Books SET title = @title WHERE id = @id", sqlConnection);//SELECT * FROM Author WHERE auth_id = @id
            sqlCommand.Parameters.AddWithValue("@title", title);
            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adp.Fill(dt); //fill the datatable ,no need to use open and close connection by using adapater

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new BookInfoModel
                {
                    id = Convert.ToInt32(dr["id"]),
                    title = Convert.ToString(dr["title"]),
                    auth_id = Convert.ToInt32(dr["author_id"]),
                    pub_id = Convert.ToInt32(dr["publisher_id"]),
                    description = Convert.ToString(dr["description"]),
                    language = Convert.ToString(dr["language"]),
                    maturityRating = Convert.ToString(dr["maturityRating"]),
                    publishedDate = Convert.ToString(dr["publishedDate"]),
                    retailPrice = Convert.ToDecimal(dr["retailPrice"])
                });
            }
            return list;

        }

        public List<BookInfoModel> DeleteBook(int id)
        {
            List<BookInfoModel> list = new List<BookInfoModel>();
            SqlCommand sqlCommand = new SqlCommand("Delete from Books where id=@id", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adp.Fill(dt); //fill the datatable ,no need to use open and close connection by using adapater

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new BookInfoModel
                {
                    id = Convert.ToInt32(dr["id"]),
                    title = Convert.ToString(dr["title"]),
                    auth_id = Convert.ToInt32(dr["author_id"]),
                    pub_id = Convert.ToInt32(dr["publisher_id"]),
                    description = Convert.ToString(dr["description"]),
                    language = Convert.ToString(dr["language"]),
                    maturityRating = Convert.ToString(dr["maturityRating"]),
                    publishedDate = Convert.ToString(dr["publishedDate"]),
                    retailPrice = Convert.ToDecimal(dr["retailPrice"])
                });
            }
            return list;

        }
    }
}
