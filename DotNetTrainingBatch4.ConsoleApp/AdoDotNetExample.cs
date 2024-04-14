using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace DotNetTrainingBatch4.ConsoleApp
{
    internal class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _stringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "ZLO\\ZLO",//servername
            InitialCatalog = "DotNetTrainingBatch4",//database name
            UserID = "sa",
            Password = "015427"
        };
        public void Read()
        {
        //SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
        //stringBuilder.DataSource = "ZLO\\ZLO";//servername
        //stringBuilder.InitialCatalog = "DotNetTrainingBatch4";//database name
        //stringBuilder.UserID = "sa";
        //stringBuilder.Password = "015427";
        //SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
        SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);

            connection.Open();
            String query = "select * from Tbl_Blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            //dataset => datatable
            //datatabel => datarow
            //datarow =>  datacolumn

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Blog ID => " + dr["BlogId"]);
                Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
                Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
                Console.WriteLine("Blog Content => " + dr["BlogContent"]);
                Console.WriteLine("-------------------------------------------");

            }
        }

        public void Creat(string title,string author,string content)
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
            connection.Open();
            string query = @"
           INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
            VALUES
           (@BlogTitle,
           @BlogAuthor,
           @BlogContent)";
            SqlCommand cmd = new SqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Saving Successful!" : "Saving Fail!";
            Console.WriteLine(message);
        }


        public void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
            connection.Open();
            string query = @"
             UPDATE [dbo].[Tbl_Blog]
             SET [BlogTitle] =  @BlogTitle
             ,[BlogAuthor] = @BlogAuthor
             ,[BlogContent] = @BlogContent
             WHERE  BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updating Successful!" : "Updating Fail!";
            Console.WriteLine(message);
            connection.Close();
        }


        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
            connection.Open();
            string query = @"
             DELETE FROM [dbo].[Tbl_Blog]
            WHERE  BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Deleting Successful!" : "Deleting Fail!";
            Console.WriteLine(message);
            connection.Close();
        }

        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);

            connection.Open();
            String query = "select * from Tbl_Blog where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found! " + id);
                return;
            }

            DataRow dr = dt.Rows[0];

            //dataset => datatable
            //datatabel => datarow
            //datarow =>  datacolumn

            
                Console.WriteLine("Blog ID => " + dr["BlogId"]);
                Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
                Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
                Console.WriteLine("Blog Content => " + dr["BlogContent"]);
                Console.WriteLine("-------------------------------------------");

            
        }
    }
}
