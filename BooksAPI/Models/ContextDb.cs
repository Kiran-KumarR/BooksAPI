using System.Data.SqlClient;
using System.Data;
//using System.Configuration;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BooksAPI.Models
{
    public class ContextDb
    {
         private string dbconn = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = BooksAPI_Db; Integrated Security = True";
         public SqlConnection sqlConnection = new SqlConnection("dbconn");

       // public SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);



        public List<AuthorModel> Get()
        {
            List<AuthorModel> list= new List<AuthorModel>();
            SqlCommand sqlCommand = new SqlCommand("select * from Author",sqlConnection);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adp.Fill(dt); //fill the datatable ,no need to use open and close connection by using adapater

            foreach(DataRow dr in dt.Rows)
            {
                list.Add(new AuthorModel
                {
                    auth_id = Convert.ToInt32(dr["0"]),
                    author_name = Convert.ToString(dr["1"])
                });
            }
            return list;

        }
    }
}
