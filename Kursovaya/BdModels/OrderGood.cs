using System;
using System.Collections.Generic;

namespace Diplom.BdModels;

public partial class OrderGood
{
    public int Idorders { get; set; }

    public int Idproduct { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Sum { get; set; }

    public virtual Order IdordersNavigation { get; set; } = null!;

    public virtual Product IdproductNavigation { get; set; } = null!;

    public string GetProductName()
    {
        return IdproductNavigation.ProductName;
    }
}
