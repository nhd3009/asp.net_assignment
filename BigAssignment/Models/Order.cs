using System;
using System.Collections.Generic;

namespace BigAssignment.Models;

public partial class Order
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public int UserId { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? ExportDate { get; set; }

    public string? DeliveryAddress { get; set; }

    public string? DeliveryName { get; set; }

    public string? DeliveryPhone { get; set; }

    public string? DeliveryEmail { get; set; }

    public int? Status { get; set; }

    public int? Trash { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }
}
