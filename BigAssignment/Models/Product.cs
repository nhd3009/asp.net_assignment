using System;
using System.Collections.Generic;

namespace BigAssignment.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Slug { get; set; }

    public int CateId { get; set; }

    public string? Image { get; set; }

    public string? ImageList { get; set; }

    public string? NewPromotion { get; set; }

    public int Installment { get; set; }

    public int Discount { get; set; }

    public string Detail { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Specification { get; set; } = null!;

    public double Price { get; set; }

    public int Quantity { get; set; }

    public double ProPrice { get; set; }

    public string? MetaKey { get; set; }

    public string? MetaDesc { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }
}
