using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLODotNetCore.ConsoleApp.Dtos;
using ZLODotNetCore.ConsoleApp.Services;

namespace ZLODotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {


        public void Run()
        {
            //Read();
            //Edit(2);
            //Edit(13);
            //Create("Zin Lin Oo", "ZIN", "ZLO007");
            //Update(1002 , "Zin Lin Oo 2", "ZIN2", "ZLO007 2");
            Delete(2);
        }

        private void Read()
        {
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst = db.Query<BlogDto>("select * from  Tbl_BLog").ToList();
            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("--------------------------");

            }

        }

        private void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogDto>("select  * from Tbl_Blog where BlogId = @BlogId ", new BlogDto { BlogId = id }).FirstOrDefault();
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

        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
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
            int result = db.Execute(query, item);
            string message = result > 0 ? "Saving Successful!" : "Saving Fail!";
            Console.WriteLine(message);

        }

        private void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string query = @"
             UPDATE [dbo].[Tbl_Blog]
             SET [BlogTitle] =  @BlogTitle
             ,[BlogAuthor] = @BlogAuthor
             ,[BlogContent] = @BlogContent
             WHERE  BlogId = @BlogId";
            IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);
            string message = result > 0 ? "Updating Successful!" : "Saving Fail!";
            Console.WriteLine(message);

        }

        private void Delete(int id)
        {
            var item = new BlogDto
            {
                BlogId = id
            };

            string query = @"
             DELETE FROM [dbo].[Tbl_Blog]
            WHERE  BlogId = @BlogId";

            IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);
            string message = result > 0 ? "Deleting Successful!" : "Deleting Fail!";
            Console.WriteLine(message);

        }
    }
}
