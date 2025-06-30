using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YWADotNetTrainingBatch2.Project1.Database.PosDbContextModels;

namespace YWADotNetTrainingBatch2.Project1.ConsoleApp.Services
{
    public class ProductService
    {
        public void Read()
        {
            PosDbContext db = new PosDbContext();
            var lst = db.TblProducts.Where(x => x.DeleteFlag == false).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine("ProductID => " + item.ProductId);
                Console.WriteLine("Product Name => " + item.PName);
                Console.WriteLine("Price => " + item.Price);
                Console.WriteLine("Create at => " + item.Createat);
            }
        }

        public void Edit()
        {
        FirstPage:
            Console.Write("Enter ProductID: ");
            var input = Console.ReadLine();
            bool isInt = int.TryParse(input, out int id);
            if (!isInt)
            {
                goto FirstPage;
            }
            PosDbContext db = new PosDbContext();
            var item = db.TblProducts.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.ProductId == id);
            if (item is null) return;
            Console.WriteLine("ProductID => " + item.ProductId);
            Console.WriteLine("Product Name => " + item.PName);
            Console.WriteLine("Price => " + item.Price);
            Console.WriteLine("Create at => " + item.Createat);
            Update(id);
        }

        public void Create()
        {
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine()!;
        PriceInput:
            Console.Write("Enter Price: ");
            var input = Console.ReadLine()!;
            bool isDecimal = decimal.TryParse(input, out decimal price);
            if (!isDecimal)
            {
                goto PriceInput;
            }
        DateInput:
            Console.Write("Enter Date(e.g., 2025-06-22 or MM/dd/yyyy): ");
            var dateInput = Console.ReadLine()!;
            bool isDateTime = DateTime.TryParse(dateInput, out DateTime createdAt);
            if (!isDateTime)
            {
                goto DateInput;
            }
            var product = new TblProduct
            {
                PName = name,
                Price = price,
                Createat = createdAt
            };

            PosDbContext db = new PosDbContext();
            db.TblProducts.Add(product);
            var result = db.SaveChanges();
            Console.WriteLine(result > 0 ? "Create Succeed" : "Create failed");
        }

        public void Update(int id)
        {
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine()!;
        PriceInput:
            Console.Write("Enter Price: ");
            var input = Console.ReadLine()!;
            bool isDecimal = decimal.TryParse(input, out decimal price);
            if (!isDecimal)
            {
                goto PriceInput;
            }
        DateInput:
            Console.WriteLine("Enter Date(e.g., 2025-06-22 or MM/dd/yyyy) : ");
            var dateInput = Console.ReadLine()!;
            bool isDateTime = DateTime.TryParse(dateInput, out DateTime createdAt);
            if (!isDateTime)
            {
                goto DateInput;
            }
            var exist = IsExist(id);
            if (!exist) return;
            PosDbContext db = new PosDbContext();
            var item = db.TblProducts.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.ProductId == id);
            if (item is null) return;
            item.PName = name;
            item.Price = price;
            item.Createat = createdAt;
            var result = db.SaveChanges();
            Console.WriteLine(result > 0 ? "Update Success" : "Update Failed");
        }

        public void Delete()
        {
        FirstPage:
            Console.WriteLine("Enter ProductID : ");
            var input = Console.ReadLine();
            bool isInt = int.TryParse(input, out int id);
            if (!isInt)
            {
                goto FirstPage;
            }
            var exit = IsExist(id);
            if (!exit) return;
            PosDbContext db = new PosDbContext();
            var item = db.TblProducts.First(x => x.ProductId == id);
            item.DeleteFlag = true;
            var result = db.SaveChanges();
            Console.WriteLine(result > 0 ? "Delete Success" : "Delete Failed");
        }

        private bool IsExist(int id)
        {
            PosDbContext db = new PosDbContext();
            var item = db.TblProducts.FirstOrDefault(x => x.ProductId == id);
            return item != null;
        }

        public void Execute()
        {
        Menu:
            Console.WriteLine("* Product Menu *");
            Console.WriteLine("======================================");
            Console.WriteLine("1. New Product");
            Console.WriteLine("2. Product List");
            Console.WriteLine("3. Edit Product");
            Console.WriteLine("4. Delete Product");
            Console.WriteLine("5. Exit");
            Console.WriteLine("======================================");
            Console.Write("Enter Product menu: ");
            string result = Console.ReadLine()!;
            if (!int.TryParse(result, out int menu))
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                goto Menu;
            }
            ProductMenuEnum productMenu = (ProductMenuEnum)menu;
            switch (productMenu)
            {
                case ProductMenuEnum.NewProduct:
                    Console.WriteLine("__________________");
                    Create();
                    Console.WriteLine("__________________");
                    break;
                case ProductMenuEnum.ProductList:
                    Console.WriteLine("__________________");
                    Read();
                    Console.WriteLine("__________________");
                    break;
                case ProductMenuEnum.EditProduct:
                    Console.WriteLine("__________________");
                    Edit();
                    Console.WriteLine("__________________");
                    break;
                case ProductMenuEnum.DeleteProduct:
                    Console.WriteLine("__________________");
                    Delete();
                    Console.WriteLine("__________________");
                    break;
                case ProductMenuEnum.Exit:
                    goto End;
                case ProductMenuEnum.None:
                default:
                    break;
            }
            goto Menu;
            
        End:
            Console.WriteLine("Exiting the Product Menu. Goodbye!");
        }
    }

    public enum ProductMenuEnum
    {
        None,
        NewProduct,
        ProductList,
        EditProduct,
        DeleteProduct,
        Exit
    }
}
