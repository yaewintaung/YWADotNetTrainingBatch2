using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YWADotNetTrainingBatch2.Project1.Database.PosDbContextModels;

namespace YWADotNetTrainingBatch2.Project1.ConsoleApp.Services
{
    public class SaleService
    {
        public void Read()
        {
            PosDbContext db = new PosDbContext();
            var lst = db.TblSales.ToList();
            foreach (var item in lst)
            {
                Console.Write("SaleID => " + item.SaleId);
                Console.Write("VoucherNo => " + item.VoucherNo);
                Console.WriteLine("Total Amount => " + item.TotalAmount);
                Console.Write("SaleDate => " + item.SaleDate);
            }
        }

        public void Edit()
        {
        FirstPage:
            Console.Write("Enter SaleID: ");
            var input = Console.ReadLine();
            bool isInt = int.TryParse(input, out int id);
            if (!isInt)
            {
                goto FirstPage;
            }
            PosDbContext db = new PosDbContext();
            var item = db.TblSales.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.SaleId == id);
            if (item is null) return;
            Console.Write("SaleID => " + item.SaleId);
            Console.Write("VoucherNo => " + item.VoucherNo);
            Console.WriteLine("Total Amount => " + item.TotalAmount);
            Console.Write("SaleDate => " + item.SaleDate);
        }

        public void Create()
        {
            Console.Write("Enter Product Voucher Number: ");
            string voucherNo = Console.ReadLine()!;
        PriceInput:
            Console.Write("Enter Total Amount: ");
            var input = Console.ReadLine()!;
            bool isDecimal = decimal.TryParse(input, out decimal total);
            if (!isDecimal)
            {
                goto PriceInput;
            }
        DateInput:
            Console.WriteLine("Enter Sale Date(e.g., 2025-06-22 or MM/dd/yyyy):: ");
            var dateInput = Console.ReadLine()!;
            bool isDateTime = DateTime.TryParse(dateInput, out DateTime saleDate);
            if (!isDateTime)
            {
                goto DateInput;
            }
            var Sale = new TblSale
            {
                VoucherNo = voucherNo,
                TotalAmount = total,
                SaleDate = saleDate,
            };

            PosDbContext db = new PosDbContext();
            db.TblSales.Add(Sale);
            var result = db.SaveChanges();
            Console.WriteLine(result > 0 ? "Create Succeed" : "Create failed");
        }

        public void Execute()
        {
        Menu:
            Console.WriteLine("* Sale Menu *");
            Console.WriteLine("======================================");
            Console.WriteLine("1. New Sale");
            Console.WriteLine("2. Sale List");
            Console.WriteLine("3. Edit Sale");
            Console.WriteLine("4. Exit");
            Console.WriteLine("======================================");
            Console.Write("Enter Sale menu: ");
            string result = Console.ReadLine()!;
            if (!int.TryParse(result, out int menu))
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                goto Menu;
            }
            SaleMenu saleMenu = (SaleMenu)menu;
            switch (saleMenu)
            {
                case SaleMenu.NewSale:
                    Console.WriteLine("__________________");
                    Create();
                    Console.WriteLine("__________________");
                    break;
                case SaleMenu.SaleList:
                    Console.WriteLine("__________________");
                    Read();
                    Console.WriteLine("__________________");
                    break;
                case SaleMenu.EditSale:
                    Console.WriteLine("__________________");
                    Edit();
                    Console.WriteLine("__________________");
                    break;
                case SaleMenu.Exit:
                    goto End;
                case SaleMenu.None:
                default:
                    break;
            }
            goto Menu;

        End:
            Console.WriteLine("Exiting the Sale Menu. Goodbye!");
        }
    }

    public enum SaleMenu
    {
        None,
        NewSale,
        SaleList,
        EditSale,
        Exit
    }
}
