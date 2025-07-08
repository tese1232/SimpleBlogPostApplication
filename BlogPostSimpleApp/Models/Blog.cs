using BlogPostSimpleApp.Models;
using System.Collections.Generic;
public class Blog
{
    public int BlogId { get; set; } // Primary Key
    public string Url { get; set; }
    public List<Post> Posts { get; set; }
}
