using BooksAPI.Models;
using System.Data.SqlClient;

namespace BooksAPI.Interface
{
    public interface IBooksInfoService
    {
        Task<List<BookInfoModel>> FetchBooksFromApiAsync();
        Task<List<BookInfoModel>> RetrieveBooksFromJson(string jsonFilePath);

        Task StoreBooksInDatabase(List<BookInfoModel> bookInfos);
        int GetOrCreatePublisherId(SqlConnection connection, string publisher_name);
        int GetUniqueBookId(SqlConnection connection);

       // Task<List<BookInfoModel>> RetrieveBooksFromJson(string jsonFilePath);

        BookInfoModel GetBookById(int id);

        BookInfoModel DeleteBookById(int id);
    }
}
