using MyMainDiplomProject.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MyMainDiplomProject.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedDateRime { get; set; }
        public virtual MyMainDiplomProjectUser User { get; set; }
        public virtual List<HashTags>? PostHashTags { get; set;}
        public virtual List<Likes>? Likes { get; set; }
        public virtual List<Files>? Files { get; set; }
        public virtual List<Comments>? Comments { get; set; }
    }
}
