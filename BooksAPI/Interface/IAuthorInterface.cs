using BooksAPI.Models;
using BooksAPI.Controllers;
using System.Data.SqlClient;

namespace BooksAPI.Interface
{
    public interface IAuthorInterface
    {
        List<AuthorModel> Get();
        List<AuthorModel> Post();
        List<AuthorModel> Get(int id);
        List<AuthorModel> Put(int id, string name);
        List<AuthorModel> Delete(int id);

        List<GetAllBooksInfo> GetAllBooksInfo();

        List<GetAllBooksInfo> GetAllBooksInfo(int id);


        List<BooksModel> RetrieveBooksFromDatabase();

        Task<List<BookInfoModel>> FetchBooksFromApiAsync();

        Task StoreBooksInDatabase(List<BookInfoModel> bookInfos);

        int GetOrCreateAuthorId(SqlConnection connection, string author_name);

        int GetOrCreatePublisherId(SqlConnection connection, string publisher_name);

        int GetUniqueBookId(SqlConnection connection);


        List<BookInfoModel> GetBooks(int id);

        List<BookInfoModel> PostBooks();

        List<BookInfoModel> PutintoBooks(int id, string title);

        List<BookInfoModel> DeleteBook(int id);

        Task<List<BookInfoModel>> RetrieveBooksFromJson(string jsonFilePath);
    }
}
