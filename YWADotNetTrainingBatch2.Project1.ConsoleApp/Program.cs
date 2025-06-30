using YWADotNetTrainingBatch2.Project1.ConsoleApp.Services;

Console.WriteLine("Welcome To Mini Pos");

ProductService productService = new ProductService();
SaleService saleService = new SaleService();
SaleDetailService saleDetailService = new SaleDetailService();
SaleFeatureService saleFeatureService = new SaleFeatureService();

Start:
Console.WriteLine("1. Products");
Console.WriteLine("2. Sales");
Console.WriteLine("3. Sale Details");
Console.WriteLine("4. Sale Feature");
Console.WriteLine("5. Exit");
Console.Write("Enter menu: ");
string result = Console.ReadLine()!;
if (!int.TryParse(result, out int no))
{
    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
    goto Start;
}
PosMenuEnum menu = (PosMenuEnum)no;


switch (menu)
{
    case PosMenuEnum.Products:
        Console.WriteLine("-----You selected Products menu-----");
        productService.Execute();
        goto Start;
    case PosMenuEnum.Sales:
        saleService.Execute();
        goto Start;
    case PosMenuEnum.SaleDetails:
        saleDetailService.Execute();
        goto Start;
    case PosMenuEnum.Buy:
        saleFeatureService.Execute();
        goto Start;
    case PosMenuEnum.Exit:
        goto End;
    case PosMenuEnum.None:
    default:
        break;
}

End:
Console.WriteLine("Thank you for using the Mini POS system. Goodbye!");

public enum PosMenuEnum
{
    None,
    Products,
    Sales,
    SaleDetails,
    Buy,
    Exit
}
