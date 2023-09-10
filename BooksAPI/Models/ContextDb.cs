using System.Data.SqlClient;
using System.Data;
//using System.Configuration;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BooksAPI.Interface;

namespace BooksAPI.Models
{
    public class ContextDb:IAuthorInterface
    {


        private string dbconn = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = BooksAPI_Db; Integrated Security = True";
        SqlConnection sqlConnection;

        // public SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
        public ContextDb()
        {
            sqlConnection = new SqlConnection(dbconn);
        }

        /// <summary>
        /// GET
        /// METHOD
        /// 
        /// </summary>
        /// <returns></returns>
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
                    auth_id = Convert.ToInt32(dr["auth_id"]),
                    author_name = Convert.ToString(dr["author_name"])
                });
            }
            return list;

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
        /// PUT METHOD BY ID
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


    }
}
