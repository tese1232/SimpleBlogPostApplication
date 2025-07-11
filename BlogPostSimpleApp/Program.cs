using BlogPostSimpleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
class Program
{
    static void Main()
    {
        using var context = new AppDbContext();
        // Add new Blog
        Console.Write("Enter blog URL: ");
        var url = Console.ReadLine();
        var blog = new Blog { Url = url };
        context.Blogs.Add(blog);
        context.SaveChanges();
        // Add a Post
        var post = new Post
        {
            Title = "Hello EF Core",
            Content = "This is my first post!",
            BlogId = blog.BlogId
        };
        context.Posts.Add(post);
        context.SaveChanges();
        // Display all blogs and posts
        var blogs = context.Blogs.Include(b => b.Posts).ToList();
        foreach (var b in blogs)
        {
            Console.WriteLine($"Blog: {b.Url}");
            foreach (var p in b.Posts)
            {
                Console.WriteLine($" Post: {p.Title} - {p.Content}");
            }
        }
    }
}