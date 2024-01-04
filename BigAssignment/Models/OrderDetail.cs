using System;
using System.Collections.Generic;

namespace BigAssignment.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public double Amount { get; set; }
}
