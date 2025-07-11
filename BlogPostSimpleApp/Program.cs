using BlogPostSimpleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        using var context = new AppDbContext();

        // STEP 1: Add 3 Users
        var users = new List<User>
        {
            new User { Name = "Tanziya", Email = "tanziya@gmail.com", PhoneNumber = "9845329852" },
            new User { Name = "Kiran", Email = "kiran@gmail.com", PhoneNumber = "9065489654" },
            new User { Name = "Suzan", Email = "suzan@gmail.com", PhoneNumber = "9876086543" }
        };

        context.users.AddRange(users);
        context.SaveChanges();

        // STEP 2: Add a Blog
        Console.Write("Enter blog URL: ");
        var url = Console.ReadLine();
        var blog = new Blog { Url = url };
        context.Blogs.Add(blog);
        context.SaveChanges();

        // STEP 3: Add a Post using one of the Users (e.g. Alice)
        var user = context.users.First(); // Alice
        var post = new Post
        {
            Title = "Hello EF Core",
            Content = "This is my first post!",
            BlogId = blog.BlogId,
            UserId = user.UserId,
            PostTypeId = 1 // Make sure PostTypeId = 1 exists in the DB
        };

        context.Posts.Add(post);
        context.SaveChanges();

        // STEP 4: Display all blogs and their posts with authors
        var blogs = context.Blogs
                           .Include(b => b.Posts)
                           .ThenInclude(p => p.User)
                           .ToList();

        foreach (var b in blogs)
        {
            Console.WriteLine($"Blog: {b.Url}");
            foreach (var p in b.Posts)
            {
                Console.WriteLine($"  Post: {p.Title} - {p.Content} (by {p.User?.Name ?? "Unknown"})");
            }
        }
    }
}