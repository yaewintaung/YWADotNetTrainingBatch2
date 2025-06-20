using Microsoft.Data.SqlClient;
using System.Data;
using YWADotNetTrainingBatch2.ConsoleApp;

Console.WriteLine("Hello, World!");

AdoDotNetService adoDotNetService = new AdoDotNetService();
DapperService dapperService = new DapperService();
EFCoreService efCoreService = new EFCoreService();
efCoreService.Create();

Console.ReadKey();