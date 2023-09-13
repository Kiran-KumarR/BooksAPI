using BooksAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Interface
{
    public interface IBooksDatabaseService
    {
       BookInfoModel RetrieveBookByIdFromDatabase(int id);

        bool DeleteBookFromDatabase(int id);




    }
}
