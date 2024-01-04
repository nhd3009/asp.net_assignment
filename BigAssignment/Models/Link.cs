using System;
using System.Collections.Generic;

namespace BigAssignment.Models;

public partial class Link
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int? TableId { get; set; }
}
