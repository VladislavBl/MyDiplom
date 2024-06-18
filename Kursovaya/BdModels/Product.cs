using System;
using System.Collections.Generic;

namespace Diplom.BdModels;

public partial class Product
{
    public int IdProduct { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal ProductPrice { get; set; }

    public byte[]? ProductPicture { get; set; }

    public string? Discription { get; set; }

    public virtual ICollection<OrderGood> OrderGoods { get; set; } = new List<OrderGood>();
}
