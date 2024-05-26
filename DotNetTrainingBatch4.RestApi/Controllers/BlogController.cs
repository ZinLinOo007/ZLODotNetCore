using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ZLODotNetCore.RestApi.Db;
using ZLODotNetCore.RestApi.Model;

namespace ZLODotNetCore.RestApi.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _db;
        public BlogController()
        {
            _db = new AppDbContext();
        }
        [HttpGet]
        public IActionResult Read()
        {
            var lst = _db.Blogs.ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var lst = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (lst is null)
            {
                return NotFound("No Data Found!");
            }
            return Ok(lst);
        }


        [HttpPost]
        public IActionResult Creaet(BlogModel blog)
        {
            _db.Blogs.Add(blog);
            var result =  _db.SaveChanges();
            string message = result > 0 ? "Saving Successful!" : "Saving Fail!";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var lst = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (lst is null)
            {
                return NotFound("No Data Found!");
            }
            lst.BlogTitle = blog.BlogTitle;
            lst.BlogContent = blog.BlogContent;
            lst.BlogAuthor = blog.BlogAuthor;
            var result = _db.SaveChanges();
            string message = result > 0 ? "Updating Successful!" : "Updating Fail!";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Path(int id , BlogModel blog)
        {
            var lst = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (lst is null)
            {
                return NotFound("No Data Found!");
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                lst.BlogTitle = blog.BlogTitle;

            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                lst.BlogContent = blog.BlogContent;

            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                 lst.BlogAuthor = blog.BlogAuthor;

            }
            var result = _db.SaveChanges();
            string message = result > 0 ? "Updating Successful!" : "Updating Fail!";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var lst = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (lst is null)
            {
                return NotFound("No Data Found!");
            }
            _db.Blogs.Remove(lst);
            var result = _db.SaveChanges();
            string message = result > 0 ? "Deleting Successful!" : "Deleting Fail!";
            return Ok(message);
        }
    }
}
