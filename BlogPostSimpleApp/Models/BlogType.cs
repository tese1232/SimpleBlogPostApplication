using System;

namespace BlogPostSimpleApp.Models
{
    public class BlogType
    {
        public int BlogTypeId { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}