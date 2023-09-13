using BooksAPI.Models;
using System.Data.SqlClient;

namespace BooksAPI.Interface
{
    public interface IBooksInfoService
    {

        Task<List<BookInfoModel>> FetchBooksFromApiAsync();
        Task StoreBooksInDatabase(List<BookInfoModel> bookInfos);

        Task<int> GetOrCreateAuthorId(SqlConnection connection, string author);
        Task<int> GetOrCreatePublisherId(SqlConnection connection, string publisherName, string publishedDate);
        int GetUniqueBookId(SqlConnection connection);

        Task<List<BookInfoModel>> RetrieveBooksFromJson(string jsonFilePath);


    }
}
