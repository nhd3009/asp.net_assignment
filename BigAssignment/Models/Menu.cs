using System;
using System.Collections.Generic;

namespace BigAssignment.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Type { get; set; } = null!;

    public string? Link { get; set; }

    public int? TableId { get; set; }

    public int? ParentId { get; set; }

    public int Orders { get; set; }

    public string Positon { get; set; } = null!;

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }
}
