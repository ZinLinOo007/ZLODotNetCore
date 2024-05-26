using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using ZLODotNetCore.RestApi.Model;

namespace ZLODotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_Blog";

            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            //List<BlogModel> lst = new List<BlogModel>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    BlogModel blog = new BlogModel();
            //    blog.BlogId = Convert.ToInt32(dr["BlogId"]);
            //    blog.BlogContent = Convert.ToString(dr["BlogContent"]);
            //    blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
            //    lst.Add(blog);

            //}
            List<BlogModel> lst = dt.AsEnumerable().Select(dr => new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"])

            }).ToList();

            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            string query = "select * from Tbl_Blog where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            if (dt.Rows.Count == 0)
            {
                return NotFound("No Data Found!");
            }

            DataRow dr = dt.Rows[0];
            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"])
            };

            return Ok(item);

        }

        [HttpPost]
        public IActionResult CreatBlog(BlogModel blog)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
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
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Saving Successful!" : "Saving Fail!";

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"
             UPDATE [dbo].[Tbl_Blog]
             SET [BlogTitle] =  @BlogTitle
             ,[BlogAuthor] = @BlogAuthor
             ,[BlogContent] = @BlogContent
             WHERE  BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updating Successful!" : "Updating Fail!";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "select  * from Tbl_Blog where BlogId = @BlogId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result =(int) cmd.ExecuteScalar();
            if(result == 0)
            {
                return NotFound("No Data Found!");
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            List<BlogModel> lst = new List<BlogModel>(); 
            if (dt.Rows.Count == 0)
            {
                return NotFound("No Data Found!");
            }

            DataRow dr = dt.Rows[0];
            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"])
            };

            lst.Add(item);

            string condition = string.Empty;
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!String.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += "[BlogTitle] = @BlogTitle,";
                parameters.Add(new SqlParameter("@BlogTitle", SqlDbType.NVarChar) { Value = blog.BlogTitle });
                item.BlogTitle = blog.BlogTitle;
            }

            if (!String.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += "[BlogAuthor] = @BlogAuthor,";
                parameters.Add(new SqlParameter("@BlogAuthor", SqlDbType.NVarChar) { Value = blog.BlogAuthor });

                item.BlogAuthor = blog.BlogAuthor;
            }

            if (!String.IsNullOrEmpty(blog.BlogContent))
            {
                condition += "[BlogContent] = @BlogContent,";
                parameters.Add(new SqlParameter("@BlogContent", SqlDbType.NVarChar) { Value = blog.BlogContent });

                item.BlogContent = blog.BlogContent;
            }

            if (condition.Length == 0)
            {
                return NotFound("No data to update!");
            }

            condition = condition.TrimEnd(',', ' ');
            blog.BlogId = id;
            query = $@"
             UPDATE [dbo].[Tbl_Blog]
             Set{condition}
             WHERE  BlogId = @BlogId";

            SqlCommand cmd2 = new SqlCommand(query, connection);
            cmd2.Parameters.AddWithValue("@BlogId", id);
            cmd2.Parameters.AddRange(parameters.ToArray());


            result = cmd2.ExecuteNonQuery();
            string message = result > 0 ? "Updating Successful!" : "Updating Fail!";
            return Ok(message);

            connection.Close();

            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"
             DELETE FROM [dbo].[Tbl_Blog]
            WHERE  BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Deleting Successful!" : "Deleting Fail!";
            return Ok(message);
             
        }
    }
}
