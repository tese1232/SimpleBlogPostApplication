using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPostSimpleApp.Models
{
   public class PostType
{
    public int PostTypeId { get; set; }
    public int Status { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<Post> Posts { get; set; }
}
}
