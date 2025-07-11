using BlogPostSimpleApp.Models;
using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<BlogType> BlogTypes { get; set; }
    public DbSet<PostType> PostTypes { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    =>
   options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BlogDb;Trusted_Connection = True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasOne(p => p.PostType)
            .WithMany(pt => pt.Posts)
            .HasForeignKey(p => p.PostTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    
        modelBuilder.Entity<Blog>()
            .HasOne(p => p.BlogType)
            .WithMany(pt => pt.Blogs)
            .HasForeignKey(p => p.BlogTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Post>()
        .HasOne(p => p.Blog)
        .WithMany(b => b.Posts)
        .HasForeignKey(p => p.BlogId)
        .OnDelete(DeleteBehavior.Cascade);


    }
}