using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YWADotNetTrainingBatch2.Project1.Database.PosDbContextModels;

namespace YWADotNetTrainingBatch2.Project1.ConsoleApp.Services
{
    public class SaleDetailService
    {
        public void Read()
        {
            PosDbContext db = new PosDbContext();
            var lst = db.TblSaleDetails
                .Where(x => x.DeleteFlag == false)
                .ToList();
            foreach (var item in lst)
            {
                Console.WriteLine("SaleDetailID => " + item.SaleDetailId);
                Console.WriteLine("SaleID => " + item.SaleId);
                Console.WriteLine("ProductID => " + item.ProductId);
                Console.WriteLine("Quantity => " + item.Quantity);
                Console.WriteLine("Price => " + item.Price);
            }
        }

        public void Edit()
        {
        FirstPage:
            Console.WriteLine("Enter SaleDetailID : ");
            var input = Console.ReadLine()!;
            bool isInt = int.TryParse(input, out int id);
            if (!isInt) goto FirstPage;
            PosDbContext db = new PosDbContext();
            var item = db.TblSaleDetails.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.SaleDetailId == id);
            if (item is null) return;
            Console.WriteLine("SaleDetailID => " + item.SaleDetailId);
            Console.WriteLine("SaleID => " + item.SaleId);
            Console.WriteLine("ProductID => " + item.ProductId);
            Console.WriteLine("Quantity => " + item.Quantity);
            Console.WriteLine("Price => " + item.Price);
        }

        public void Create()
        {
            int saleId = ReadInt("Enter SaleID: ");
            int productId = ReadInt("Enter ProductID: ");
            int quantity = ReadInt("Enter Quantity: ");
        PriceInput:
            Console.Write("Enter Price: ");
            var input = Console.ReadLine()!;
            bool isDecimal = decimal.TryParse(input, out decimal price);
            if (!isDecimal)
            {
                goto PriceInput;
            }
            var saleDetail = new TblSaleDetail
            {
                SaleId = saleId,
                ProductId = productId,
                Quantity = quantity,
                Price = price,
            };
            PosDbContext db = new PosDbContext();
            db.TblSaleDetails.Add(saleDetail);
            var result = db.SaveChanges();
            Console.WriteLine(result > 0 ? "Create Succeed" : "Create failed");
        }

        public int ReadInt(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()!;
                if (int.TryParse(input, out value))
                {
                    return value;
                }
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        public void Execute()
        {
        Menu:
            Console.WriteLine("* Sale Detail Menu *");
            Console.WriteLine("======================================");
            Console.WriteLine("1. New Sale Detail");
            Console.WriteLine("2. Sale Detail List");
            Console.WriteLine("3. Edit Sale Detail");
            Console.WriteLine("4. Exit");
            Console.WriteLine("======================================");
            Console.Write("Enter Sale Detail menu: ");
            string result = Console.ReadLine()!;
            if (!int.TryParse(result, out int menu))
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                goto Menu;
            }
            SaleDetailMenuEnum saleMenu = (SaleDetailMenuEnum)menu;
            switch (saleMenu)
            {
                case SaleDetailMenuEnum.NewSaleDetail:
                    Console.WriteLine("__________________");
                    Create();
                    Console.WriteLine("__________________");
                    break;
                case SaleDetailMenuEnum.SaleDetailList:
                    Console.WriteLine("__________________");
                    Read();
                    Console.WriteLine("__________________");
                    break;
                case SaleDetailMenuEnum.EditSaleDetail:
                    Console.WriteLine("__________________");
                    Edit();
                    Console.WriteLine("__________________");
                    break;
                case SaleDetailMenuEnum.Exit:
                    goto End;
                case SaleDetailMenuEnum.None:
                default:
                    break;
            }
            goto Menu;

        End:
            Console.WriteLine("Exiting the Sale Detial Menu. Goodbye!");
        }
    }

    public enum SaleDetailMenuEnum
    {
        None,
        NewSaleDetail,
        SaleDetailList,
        EditSaleDetail,
        Exit
    }
}

