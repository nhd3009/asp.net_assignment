using System;
using System.Collections.Generic;

namespace BigAssignment.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Slug { get; set; }

    public int? ParentId { get; set; }

    public int Orders { get; set; }

    public string? MetaKey { get; set; }

    public string? MetaDesc { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }
}
