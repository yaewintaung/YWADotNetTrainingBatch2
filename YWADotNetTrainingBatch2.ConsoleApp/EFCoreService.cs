using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YWADotNetTrainingBatch2.ConsoleApp
{
    internal class EFCoreService
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            List<BlogModel> list = db.Blogs.Where(b=>b.DeleteFlag == false).ToList();
            foreach (var item in list)
            {
                Console.WriteLine($"ID: {item.BlogId}, Title: {item.BlogTitle}, Author: {item.BlogAuthor}");
                Console.WriteLine("______________________");
            }
        }

        public void Edit()
        {
            Console.Write("Please enter the blog ID to edit: ");
            string input = Console.ReadLine()!;
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("Invalid ID format. Please try again.");
                return;
            }
            AppDbContext db = new AppDbContext();
            BlogModel? item = db.Blogs.Where(b => b.DeleteFlag == false).FirstOrDefault(b => b.BlogId == id);
            if (item == null)
            {
                Console.WriteLine("Blog not found. Please try again.");
                return;
            }
            Console.WriteLine($"ID: {item.BlogId}, Title: {item.BlogTitle}, Author: {item.BlogAuthor}");

        }

        public void Create()
        {
            Console.Write("Please enter the blog title: ");
            string title = Console.ReadLine()!;
            Console.Write("Please enter the blog author: ");
            string author = Console.ReadLine()!;
            Console.Write("Please enter the blog content: ");
            string content = Console.ReadLine()!;
            BlogModel blog = new BlogModel
            {   
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };

            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            int result = db.SaveChanges();
            Console.WriteLine(result > 0 ? "Blog created successfully." : "Failed to create blog.");
        }
        
        public void Update()
        {
            Console.Write("Please enter the blog ID to update: ");
            string input = Console.ReadLine()!;
            if (!int.TryParse(input, out int id) || !FindId(id))
            {
                Console.WriteLine("Invalid ID format or Item not found. Please try again.");
                return;
            }

            
            Console.WriteLine("Update Title: ");
            string title = Console.ReadLine()!;
            Console.WriteLine("Update Author: ");
            string author = Console.ReadLine()!;
            Console.WriteLine("Update Content: ");
            string content = Console.ReadLine()!;
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(b => b.BlogId == id);
            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;
            db.SaveChanges();

        }

        public void Delete()
        {
            Console.Write("Please enter the blog ID to delete: ");
            string input = Console.ReadLine()!;
            if (!int.TryParse(input, out int id) || !FindId(id))
            {
                Console.WriteLine("Invalid ID format or Item not found. Please try again.");
                return;
            }
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(b => b.BlogId == id);
            //db.Blogs.Remove(item);
            item.DeleteFlag = true; // Soft delete
            db.SaveChanges();
            Console.WriteLine("Blog deleted successfully.");
        }

        private bool FindId(int id)
        {
            AppDbContext appDbContext = new AppDbContext();
            BlogModel? blog = appDbContext.Blogs.Where(b => b.DeleteFlag == false).FirstOrDefault(b=> b.BlogId == id);
            return blog != null;
        }
    }
}
