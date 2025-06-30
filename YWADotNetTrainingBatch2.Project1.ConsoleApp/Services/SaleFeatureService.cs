using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YWADotNetTrainingBatch2.Project1.Database.PosDbContextModels;

namespace YWADotNetTrainingBatch2.Project1.ConsoleApp.Services
{
    public class SaleFeatureService
    {
        public void Execute()
        {
            PosDbContext dbContext = new PosDbContext();

            List<TblSaleDetail> products = new List<TblSaleDetail>(); 
            Console.WriteLine("-----You selected Sale Feature menu-----");
        FirstPage:
            Console.Write("Please Enter Product Id:");
            if(!int.TryParse(Console.ReadLine(),out int id))
            {
                Console.WriteLine("Id is not correct");
                goto FirstPage;
            }

            var item = dbContext.TblProducts.FirstOrDefault(p=>p.ProductId == id);
            if(item == null)
            {
                Console.WriteLine("Product not found");
                goto FirstPage;
            }
            LogProduct(item);
        QuantityInput:
            Console.Write($"Product Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Quantity is not correct");
                goto QuantityInput;
            }
            products.Add(new TblSaleDetail
            {
                DeleteFlag = false,
                Price = item.Price,
                ProductId = item.ProductId,
                Quantity = quantity
            });
            Console.WriteLine("Are you sure want to add more? Y/N");
            string? res = Console.ReadLine();
            if (res == "Y")
            {
                goto FirstPage;
            }

            TblSale sale = new TblSale()
            {
                TotalAmount = products.Sum(x => x.Price * x.Quantity),
                SaleDate = DateTime.Now,
                DeleteFlag = false,
                VoucherNo = Guid.NewGuid().ToString(),
            };
            dbContext.TblSales.Add(sale);
            dbContext.SaveChanges();
            foreach (var product in products)
            {
                product.SaleId = sale.SaleId;
            }

            dbContext.TblSaleDetails.AddRange(products);
            dbContext.SaveChanges();
        }

        public void LogProduct(TblProduct product)
        {
            Console.WriteLine($"Product Name: {product.PName}");
            Console.WriteLine($"Product Price: {product.Price}");
        }
    }
}
