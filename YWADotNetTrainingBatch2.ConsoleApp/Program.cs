using Microsoft.Data.SqlClient;
using System.Data;
using YWADotNetTrainingBatch2.ConsoleApp;

Console.WriteLine("Hello, World!");

AdoDotNetService adoDotNetService = new AdoDotNetService();
adoDotNetService.Update();

Console.ReadKey();