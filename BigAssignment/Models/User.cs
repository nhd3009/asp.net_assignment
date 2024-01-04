using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BigAssignment.Models;

public partial class User
{
    [Key, Column(Order = 1)]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string? Fullname { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string? Name { get; set; }
    
    public string? Password { get; set; }
   /* [Required]
    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]*/
    public string? Email { get; set; }

    public int Gender { get; set; }

    public int Phone { get; set; }

    public string? Address { get; set; }

    public string? Image { get; set; }

    public int Access { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }
}
