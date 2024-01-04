using System;
using System.Collections.Generic;

namespace BigAssignment.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string? Fullname { get; set; }

    public string? Email { get; set; }

    public int Phone { get; set; }

    public string? Title { get; set; }

    public string? Detail { get; set; }

    public int Flag { get; set; }

    public string? Reply { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }
}
