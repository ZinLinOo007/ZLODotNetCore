using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLODotNetCore.ConsoleApp.Services;

namespace ZLODotNetCore.ConsoleApp.EFCoreExamples
{
    internal class EFCoreExample
    {

        private readonly AppDbContext db = new AppDbContext();
        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(3);
            //Create("ZLO", "ZIN LIN OO", "007");
            //Update(1003, "Z", "Z", "Z");
            Delete(3);
        }

        private void Read()
        {
            var lst = db.Blogs.ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-----------------------");


            }
        }


        private void Edit(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item != null)
            {
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("--------------------------");
            }
            else
            {
                Console.WriteLine("No Data Found!");
                return;
            }
        }

        private void Create(string title, string auhor, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = auhor,
                BlogContent = content
            };
            db.Blogs.Add(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "Saving Successful!" : "Saving Fail!";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No Data Found!");
                return;
            }
            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;
            int result = db.SaveChanges();
            string message = result > 0 ? "Updating Successful!" : "Saving Fail!";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No Data Found!");
                return;
            }

            db.Blogs.Remove(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "Deleting Successful!" : "Deleting Fail!";
            Console.WriteLine(message);
        }
    }
}
