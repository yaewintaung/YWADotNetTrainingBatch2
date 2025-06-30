using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using YWADotNetTrainingBatch2.ConsoleApp;
using YWADotNetTrainingBatch2.Database;

Console.WriteLine("Hello, World!");

AdoDotNetService adoDotNetService = new AdoDotNetService();
DapperService dapperService = new DapperService();
EFCoreService efCoreService = new EFCoreService();

Console.ReadKey();