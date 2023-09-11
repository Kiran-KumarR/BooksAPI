using BooksAPI.Interface;
using BooksAPI.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace BooksAPI.Services
{
    public class BooksDatabaseService:IBooksDatabaseService
    {
        public string dbconn = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = BooksAPI_Db; Integrated Security = True";
        SqlConnection sqlConnection;

        // public SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
        public BooksDatabaseService()
        {
            sqlConnection = new SqlConnection(dbconn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BookInfoModel RetrieveBookByIdFromDatabase(int id)
        {
            try
            {
                //string connectionString = _configuration.GetConnectionString("dbconn");

                using (SqlConnection connection = new SqlConnection(dbconn))
                {
                    connection.Open();

                    string selectSql = " select B.* ,A.author_name,P.publisher_name from Books B inner join  Author A  ON  B.author_id = A.auth_id inner join Publisher P on B.publisher_id = P.pub_id where B.id=@id";
                    SqlCommand command = new SqlCommand(selectSql, connection);
                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        var book = new BookInfoModel
                        { 
                            id = reader.GetInt32(0),
                            title = reader.GetString(1),
                            author_id=reader.GetInt32(2),
                            publisher_id = reader.GetInt32(3),
                            description = reader.GetString(4),
                            language=reader.GetString(5),
                            maturityRating=reader.GetString(6),
                            pageCount=reader.GetInt32(7),
                            publishedDate=reader.GetString(8), 
                            retailPrice=reader.GetDecimal(9),

                            author_name = reader.GetString(10),
                            publisher_name = reader.GetString(11)
                           
                           
                        };

                        return book;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public bool DeleteBookFromDatabase(int id)
        {
            try
            {

                //string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(dbconn))
                {
                    connection.Open();

                    string deleteSql = "DELETE FROM Books WHERE id = @id";
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }


        public List<BookInfoModel> RetrieveBooksFromDatabase()
        {
            List<BookInfoModel> books = new List<BookInfoModel>();

            try
            {
                //string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(dbconn))
                {
                    connection.Open();

                    string selectSql = " select B.* ,A.author_name,P.publisher_name from Books B inner join  Author A  ON  B.author_id = A.auth_id inner join Publisher P on B.publisher_id = P.pub_id ";
                    SqlCommand command = new SqlCommand(selectSql, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var book = new BookInfoModel
                            {
                                id = reader.GetInt32(0),
                                title = reader.GetString(1),
                                author_id = reader.GetInt32(2),
                                publisher_id = reader.GetInt32(3),
                                description = reader.GetString(4),
                                language = reader.GetString(5),
                                maturityRating = reader.GetString(6),
                                pageCount = reader.GetInt32(7),
                                publishedDate = reader.GetString(8),
                                retailPrice = reader.GetDecimal(9),

                                author_name = reader.GetString(10),
                                publisher_name = reader.GetString(11)

                            };

                            books.Add(book);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return books;
        }*/
    public class BooksDatabaseService:IBooksDatabaseService
    {
        public string dbconn = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = BooksAPI_Db; Integrated Security = True";
        SqlConnection sqlConnection;

        // public SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
        public BooksDatabaseService()
        {
            sqlConnection = new SqlConnection(dbconn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BookInfoModel RetrieveBookByIdFromDatabase(int id)
        {
            try
            {
                //string connectionString = _configuration.GetConnectionString("dbconn");

                using (SqlConnection connection = new SqlConnection(dbconn))
                {
                    connection.Open();

                    string selectSql = " select B.* ,A.author_name,P.publisher_name from Books B inner join  Author A  ON  B.author_id = A.auth_id inner join Publisher P on B.publisher_id = P.pub_id where B.id=@id";
                    SqlCommand command = new SqlCommand(selectSql, connection);
                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        var book = new BookInfoModel
                        { 
                            id = reader.GetInt32(0),
                            title = reader.GetString(1),
                            author_id=reader.GetInt32(2),
                            publisher_id = reader.GetInt32(3),
                            description = reader.GetString(4),
                            language=reader.GetString(5),
                            maturityRating=reader.GetString(6),
                            pageCount=reader.GetInt32(7),
                            publishedDate=reader.GetString(8), 
                            retailPrice=reader.GetDecimal(9),

                            author_name = reader.GetString(10),
                            publisher_name = reader.GetString(11)
                           
                           
                        };

                        return book;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public bool DeleteBookFromDatabase(int id)
        {
            try
            {

                //string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(dbconn))
                {
                    connection.Open();

                    string deleteSql = "DELETE FROM Books WHERE id = @id";
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }


        public List<BookInfoModel> RetrieveBooksFromDatabase()
        {
            List<BookInfoModel> books = new List<BookInfoModel>();

            try
            {
                //string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(dbconn))
                {
                    connection.Open();

                    string selectSql = " select B.* ,A.author_name,P.publisher_name from Books B inner join  Author A  ON  B.author_id = A.auth_id inner join Publisher P on B.publisher_id = P.pub_id ";
                    SqlCommand command = new SqlCommand(selectSql, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var book = new BookInfoModel
                            {
                                id = reader.GetInt32(0),
                                title = reader.GetString(1),
                                author_id = reader.GetInt32(2),
                                publisher_id = reader.GetInt32(3),
                                description = reader.GetString(4),
                                language = reader.GetString(5),
                                maturityRating = reader.GetString(6),
                                pageCount = reader.GetInt32(7),
                                publishedDate = reader.GetString(8),
                                retailPrice = reader.GetDecimal(9),

                                author_name = reader.GetString(10),
                                publisher_name = reader.GetString(11)

                            };

                            books.Add(book);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return books;
        }

    }
}
