﻿using BlogPostSimpleApp.Models;
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

        var blogType1 = new BlogType { Status = 1, Name = "john", Description = "Elder Brother" };
        var blogType2 = new BlogType { Status = 2, Name = "donald", Description = "Cousin Brother" };
        context.BlogTypes.AddRange(blogType1, blogType2);
        context.SaveChanges();


        var status1 = new Status { StatusCode = 1, Name = "Active", Description = "Active status" };
        var status2 = new Status { StatusCode = 2, Name = "Inactive", Description = "Inactive status" };
        context.Statuses.AddRange(status1, status2);
        context.SaveChanges();


        var blog1 = new Blog { Url = "www.johncom", isPublic = true, BlogTypeId = blogType1.BlogTypeId, StatusId = status1.StatusId };
        var blog2 = new Blog { Url = "www.donald.com", isPublic = true, BlogTypeId = blogType2.BlogTypeId, StatusId = status2.StatusId };
        context.Blogs.AddRange(blog1, blog2);
        context.SaveChanges();


        var postType1 = new PostType { Status = 1, Name = "toronton", Description = "City Name" };
        var postType2 = new PostType { Status = 2, Name = "Canada", Description = "The Best Country" };
        context.PostTypes.AddRange(postType1, postType2);
        context.SaveChanges();


        var user1 = new User { Name = "jophn", Email = "john20@gmail.com", PhoneNumber = "6758465733" };
        var user2 = new User { Name = "donaldz", Email = "donald20@gmail.com", PhoneNumber = "567876543" };
        context.users.AddRange(user1, user2);
        context.SaveChanges();


        var post1 = new Post { Title = "How to become Rich", Content = "Details Included in the Bio", BlogId = blog1.BlogId, PostTypeId = postType1.PostTypeId, UserId = user1.UserId };
        var post2 = new Post { Title = "How to become Developer", Content = "By Lots of Practicing", BlogId = blog2.BlogId, PostTypeId = postType2.PostTypeId, UserId = user2.UserId };
        context.Posts.AddRange(post1, post2);
        context.SaveChanges();



        var results = context.Posts
       .Include(p => p.Blog)
       .ThenInclude(b => b.BlogType)
       .Include(p => p.Blog.Status)
       .Include(p => p.PostType)
       .Include(p => p.User)
       .Where(p =>
              p.PostType.Status == 1 &&
              p.Blog.BlogType.Status == 1 &&
              p.Blog.Status.Name == "Active")
       .Select(p => new
       {
           BlogUrl = p.Blog.Url,
           BlogIsPublic = p.Blog.isPublic,
           BlogTypeName = p.Blog.BlogType.Name,
           UserName = p.User.UserName,
           UserEmail = p.User.Email,
           TotalUserPosts = context.Posts.Count(x => x.UserId == p.UserId),
           PostTypeName = p.PostType.Name
       })
       .OrderBy(p => p.UserName)
       .ToList();


        foreach (var item in results)
        {
            Console.WriteLine("==================================");
            Console.WriteLine($"Blog URL: {item.BlogUrl}");
            Console.WriteLine($"Is Public: {item.BlogIsPublic}");
            Console.WriteLine($"Blog Type: {item.BlogTypeName}");
            Console.WriteLine($"User Name: {item.UserName}");
            Console.WriteLine($"User Email: {item.UserEmail}");
            Console.WriteLine($"Total Posts by User: {item.TotalUserPosts}");
            Console.WriteLine($"Post Type: {item.PostTypeName}");
        }
    }
}