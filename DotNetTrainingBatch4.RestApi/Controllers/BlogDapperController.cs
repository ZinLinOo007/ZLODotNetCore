using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ZLODotNetCore.RestApi.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ZLODotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        //Read
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_Blog";
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            List<BlogModel> lst = db.Query<BlogModel>(query).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            //string query = "select  * from Tbl_Blog where BlogId = @BlogId ";
            //using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            //var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
            var item = FindById(id);
            if (item != null)
            {
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("--------------------------");
            }
            else
            {
                return NotFound("No Data Found!");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBLog(BlogModel blog)
        {
            string query = @"
           INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
            VALUES
           (@BlogTitle,
           @BlogAuthor,
           @BlogContent)";
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Saving Successful!" : "Saving Fail!";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBLog(int id,BlogModel blog)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No Data Found!");
            }
            string query = @"
             UPDATE [dbo].[Tbl_Blog]
             SET [BlogTitle] =  @BlogTitle
             ,[BlogAuthor] = @BlogAuthor
             ,[BlogContent] = @BlogContent
             WHERE  BlogId = @BlogId";
            IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Updating Successful!" : "Saving Fail!";
            return Ok(message);
        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No Data Found!");
            }
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += "[BlogTitle] = @BlogTitle,";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                condition += "[BlogContent] = @BlogContent,";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += "[BlogAuthor] = @BlogAuthor";
            }
            if(condition.Length == 0)
            {
                return NotFound("No data to update!");
            }
            condition = condition.Substring(0, condition.Length - 2);
            blog.BlogId = id;
            string query = $@"
             UPDATE [dbo].[Tbl_Blog]
             Set{condition}
             WHERE  BlogId = @BlogId";
            IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Updating Successful!" : "Saving Fail!";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No Data Found!");
            }
            string query = @"
             DELETE FROM [dbo].[Tbl_Blog]
            WHERE  BlogId = @BlogId";

            IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, new BlogModel { BlogId = id});
            string message = result > 0 ? "Deleting Successful!" : "Deleting Fail!";
            return Ok(message);
        }

        private BlogModel FindById(int id)
        {
            string query = "select  * from Tbl_Blog where BlogId = @BlogId ";
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
            return item;
        }


    }
}
