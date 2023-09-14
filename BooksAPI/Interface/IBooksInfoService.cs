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
        int GetOrCreateBookId(SqlConnection connection);

       // Task<List<BookInfoModel>> RetrieveBooksFromJson(string jsonFilePath);

        BookInfoModel GetBookById(int id);

        BookInfoModel DeleteBookById(int id);

        List<BookInfoModel> PostBooks(BookInfoModel bookInfo);

        List<BookInfoModel> PutintoBooks(BookInfoModel bookInfo);

        int GetBookId(SqlConnection connection);


    }
}
