using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YWADotNetTrainingBatch2.ConsoleApp
{
    public class DapperService
    {
        private SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = ".",
            InitialCatalog = "DotNetTrainingBatch2",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true,
        };

        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                var list = db.Query<BlogDto>("select * from Tbl_Blog").ToList();
                foreach (var item in list)
                {
                    Console.WriteLine($" ID: {item.BlogId},\n Title: {item.BlogTitle},\n Author: {item.BlogAuthor}");
                    Console.WriteLine("______________________");
                }
            }
        }

        public void Edit()
        {
        FirstPhase:
            Console.Write("Please enter the blog ID to edit: ");
            string input = Console.ReadLine()!;
            if(!int.TryParse(input,out int id))
            {
                goto FirstPhase;
            }
            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                var item = db.QueryFirstOrDefault<BlogDto>("select * from Tbl_Blog where BlogId = @BlogId", new BlogDto { BlogId = id});
                if(item == null)
                {
                    Console.WriteLine("Blog not found. Please try again.");
                    return;
                }

                Console.WriteLine($" ID: {item.BlogId},\n Title: {item.BlogTitle},\n Author: {item.BlogAuthor}");
            }
        }

        public void Create()
        {
            Console.Write("Please enter the blog title: ");
            string title = Console.ReadLine()!;
            Console.Write("Please enter the blog author: ");
            string author = Console.ReadLine()!;
            Console.Write("Please enter the blog content: ");
            string content = Console.ReadLine()!;
            BlogDto blog = new BlogDto
            {
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };

            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                                   ([BlogTitle]
                                   ,[BlogContent]
                                   ,[BlogAuthor])
                             VALUES
                                   (@BlogTitle,@BlogContent,@BlogAuthor)";

            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                int result = db.Execute(query, blog);
                Console.WriteLine(result > 0 ? "Blog created successfully." : "Failed to create blog.");
            }

        }

        public void Update()
        {
        FirstPhase:
            Console.Write("Please enter the blog ID to Update: ");
            string input = Console.ReadLine()!;
            if (!int.TryParse(input, out int id))
            {
                goto FirstPhase;
            }
            Console.Write("Please enter new blog title: ");
            string title = Console.ReadLine()!;
            Console.Write("Please enter new blog author: ");
            string author = Console.ReadLine()!;
            Console.Write("Please enter new blog content: ");
            string content = Console.ReadLine()!;
            BlogDto blog = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                               SET [BlogTitle] = @BlogTitle
                                  ,[BlogContent] = @BlogContent
                                  ,[BlogAuthor] = @BlogAuthor
                             WHERE BlogId = @BlogId";

            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                int result = db.Execute(query, blog);
                Console.WriteLine(result > 0 ? "Blog updated successfully." : "Failed to updated blog.");
            }
        }

        public void Delete()
        {
        FirstPhase:
            Console.Write("Please enter the blog ID to Update: ");
            string input = Console.ReadLine()!;
            if (!int.TryParse(input, out int id))
            {
                goto FirstPhase;
            }
            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                var item = db.QueryFirstOrDefault<BlogDto>("select * from Tbl_Blog where BlogId = @BlogId", new BlogDto { BlogId = id });
                if (item == null)
                {
                    Console.WriteLine("Blog not found. Please try again.");
                    return;
                }
                string query = $@"DELETE FROM [dbo].[Tbl_Blog]
                                  WHERE BlogId = @BlogId";
                int result = db.Execute(query, new { BlogId = id });
                Console.WriteLine(result > 0 ? "Blog deleted successfully." : "Failed to deleted blog.");
            }
        }

    }
}
