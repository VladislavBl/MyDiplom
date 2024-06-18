using System;
using System.Collections.Generic;

namespace Diplom.BdModels;

public partial class Order
{
    public int IdOrder { get; set; }

    public int OrderNumber { get; set; }

    public DateOnly OrderDate { get; set; }

    public int Buyer { get; set; }

    public decimal OrderSum { get; set; }

    public virtual User BuyerNavigation { get; set; } = null!;

    public virtual ICollection<OrderGood> OrderGoods { get; set; } = new List<OrderGood>();
}
