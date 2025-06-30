using System;
using System.Collections.Generic;

namespace YWADotNetTrainingBatch2.Project1.Database.PosDbContextModels;

public partial class TblProduct
{
    public int ProductId { get; set; }

    public string PName { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime? Createat { get; set; }

    public bool DeleteFlag { get; set; }
}
