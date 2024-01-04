using System;
using System.Collections.Generic;

namespace BigAssignment.Models;

public partial class Slider
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public string? Position { get; set; }

    public string? Image { get; set; }

    public int Orders { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }
}
