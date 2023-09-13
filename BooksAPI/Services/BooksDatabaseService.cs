using BooksAPI.Interface;
using BooksAPI.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace BooksAPI.Services
{
    public class BooksDatabaseService// IBooksDatabaseService
    {/*
        private readonly IConfiguration _configuration;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public BooksDatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
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
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectSql = " select B.* ,A.author_name,P.publisher_name from Books B inner join  Author A  ON  B.author_id = A.auth_id inner join Publisher P on B.publisher_id = P.pub_id where B.id='@id'";
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
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
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


        public List<Book> RetrieveBooksFromDatabase()
        {
            List<Book> books = new List<Book>();

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectSql = " SELECT b.book_id, b.title, a.author_name, p.publisher_name, p.published_date, b.description\r\n FROM Books b\r\n INNER JOIN Authors a ON b.author_id = a.author_id\r\n INNER JOIN Publishers p ON b.publisher_id = p.publisher_id";
                    SqlCommand command = new SqlCommand(selectSql, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var book = new Book
                            {
                                publisher_id = reader.GetInt32(0),
                                title = reader.GetString(1),
                                author_name = reader.GetString(2),
                                publisher_name = reader.GetString(3),
                                published_date = reader.GetString(4),
                                description = reader.GetString(5),

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

    }
}
