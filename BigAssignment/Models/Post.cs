using System;
using System.Collections.Generic;

namespace BigAssignment.Models;

public partial class Post
{
    public int Id { get; set; }

    public int TopicId { get; set; }

    public string? Slug { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Detail { get; set; }

    public string? Image { get; set; }

    public string? Type { get; set; }

    public string? Position { get; set; }

    public string? MetaKey { get; set; }

    public string? MetaDesc { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }
}
