using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPostSimpleApp.Models
{
    [Table("BlogType")]
    public class BlogType
    {
        public int BlogTypeId { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}