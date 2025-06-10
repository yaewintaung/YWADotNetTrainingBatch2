using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YWADotNetTrainingBatch2.ConsoleApp
{
    public class AdoDotNetService
    {
        SqlConnectionStringBuilder _connectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = ".",
            InitialCatalog = "DotNetTrainingBatch2",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true, 
        };
        public void Read()
        {
            

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string query = "select * from Tbl_Blog";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            connection.Close();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                Console.WriteLine($"ID: {row["BlogTitle"]}, Name: {row["BlogAuthor"]}");
            }


        }

        public void Edit()
        {
            Console.Write("Please enter the blog ID to edit:");
            string blogId = Console.ReadLine()!;

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string query = $"select * from Tbl_Blog where BlogId = @bid";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@bid", blogId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            connection.Close();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                Console.WriteLine($"ID: {row["BlogTitle"]}, Name: {row["BlogAuthor"]}");
            }


        }

        public void Create()
        {
            Console.Write("Please enter the blog title:");
            string title = Console.ReadLine()!;

            Console.Write("Please enter the blog content:");
            string content = Console.ReadLine()!;

            Console.Write("Please enter the blog author:");
            string author = Console.ReadLine()!;

            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogContent]
           ,[BlogAuthor])
     VALUES
           (@title
           ,@content
           ,@author";
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.Parameters.AddWithValue("@author", author);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            Console.WriteLine(result > 0 ? "Record created successfully." : "Failed to create record.");
        }

        public void Update()
        {
            Console.Write("Please enter the blog ID to update:");
            string blogId = Console.ReadLine()!;
            Console.Write("Please enter the new blog title:");
            string title = Console.ReadLine()!;
            Console.Write("Please enter the new blog content:");
            string content = Console.ReadLine()!;
            Console.Write("Please enter the new blog author:");
            string author = Console.ReadLine()!;

            string query = @"
               UPDATE [dbo].[Tbl_Blog]
               SET [BlogTitle] = @bid
                  ,[BlogContent] = @title
                  ,[BlogAuthor] = @author
               WHERE BlogId = @bid";
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@bid", blogId);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.Parameters.AddWithValue("@author", author);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine(result > 0 ? "Record updated successfully." : "Failed to update record.");
            Read();

        }

        public void Delete()
        {
            Console.Write("Please enter the blog ID to delete:");
            string blogId = Console.ReadLine()!;
            string query = $"DELETE FROM Tbl_Blog WHERE BlogId = @bid";
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@bid", blogId);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine(result > 0 ? "Record deleted successfully." : "Failed to delete record.");
        }
    }
}
