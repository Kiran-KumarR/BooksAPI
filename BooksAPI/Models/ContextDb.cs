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
        //string connectionString = _configuration.GetConnectionString("DefaultConnection");
        //SqlConnection sqlConnection = new SqlConnection(connectionString);

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
        ///  PUT METHOD BY ID
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
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        public List<GetAllBooksInfo> GetAllBooksInfo()
        {
            List<GetAllBooksInfo> list = new List<GetAllBooksInfo>();
            SqlCommand sqlCommand = new SqlCommand("EXEC GetAllBooksInfo", sqlConnection);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adp.Fill(dt); //fill the datatable ,no need to use open and close connection by using adapater

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new GetAllBooksInfo
                {
                    id = Convert.ToInt32(dr["id"]),
                    title = Convert.ToString(dr["title"]),
                    auth_name = Convert.ToString(dr["auth_name"]),
                    publisher_name = Convert.ToString(dr["publisher_name"]),
                    description = Convert.ToString(dr["description"]),
                    language = Convert.ToString(dr["language"]),
                    maturityRating = Convert.ToString(dr["maturityRating"]),
                    pageCount = Convert.ToInt32(dr["pageCount"]),
                    categories = Convert.ToString(dr["categories"]),
                    publishedDate = Convert.ToString(dr["publishedDate"]),
                    retailPrice = Convert.ToDecimal(dr["retailPrice"])


                });
            }
            return list;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public List<GetAllBooksInfo> GetAllBooksInfo(int id)
        {
            List<GetAllBooksInfo> list = new List<GetAllBooksInfo>();
            SqlCommand sqlCommand = new SqlCommand("GetAllBooksInfoById", sqlConnection);
            //Sqlcommand sqlCommand = new SqlCommand("GetFullNameById", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", id);
            //connection.Open();
            //sqlCommand.Parameters.AddWithValue("@id", id);
            sqlConnection.Open();
             var reader = sqlCommand.ExecuteReader();

            
               if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                list.Add(new GetAllBooksInfo
                {
                    id = Convert.ToInt32(reader.GetValue(0)),
                    title = Convert.ToString(reader.GetValue(1)),
                    auth_name = Convert.ToString(reader.GetValue(2)),
                    publisher_name = Convert.ToString(reader.GetValue(3)),
                    description = Convert.ToString(reader.GetValue(4)),
                    language = Convert.ToString(reader.GetValue(5)),
                    maturityRating = Convert.ToString(reader.GetValue(6)),
                    pageCount = Convert.ToInt32(reader.GetValue(7)),
                    categories = Convert.ToString(reader.GetValue(8)),
                    publishedDate = Convert.ToString(reader.GetValue(9)),
                    retailPrice = Convert.ToDecimal(reader.GetValue(10))


                });
            
                  }

            }
            return list;

        }

        /*SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

        DataTable dt = new DataTable();
        adp.Fill(dt); //fill the datatable ,no need to use open and close connection by using adapater

        foreach (DataRow dr in dt.Rows)
        {
            list.Add(new GetAllBooksInfo
            {
                id = Convert.ToInt32(dr["id"]),
                title = Convert.ToString(dr["title"]),
                auth_name = Convert.ToString(dr["auth_name"]),
                publisher_name = Convert.ToString(dr["publisher_name"]),
                description = Convert.ToString(dr["description"]),
                language = Convert.ToString(dr["language"]),
                maturityRating = Convert.ToString(dr["maturityRating"]),
                pageCount = Convert.ToInt32(dr["pageCount"]),
                categories = Convert.ToString(dr["categories"]),
                publishedDate = Convert.ToString(dr["publishedDate"]),
                retailPrice = Convert.ToDecimal(dr["retailPrice"])


            });
        }
        return list;*/




    }
}
