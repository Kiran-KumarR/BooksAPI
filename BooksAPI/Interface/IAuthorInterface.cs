using BooksAPI.Models;
using BooksAPI.Controllers;

namespace BooksAPI.Interface
{
    public interface IAuthorInterface
    {
        List<AuthorModel> Get();
        List<AuthorModel> Post();
        List<AuthorModel> Get(int id);
        List<AuthorModel> Put(int id, string name);
        List<AuthorModel> Delete(int id);

    }
}
