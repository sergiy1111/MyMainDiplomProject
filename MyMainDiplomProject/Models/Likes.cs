using MyMainDiplomProject.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MyMainDiplomProject.Models
{
    public class Likes
    { 

        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual Post Post { get; set; }
        public virtual MyMainDiplomProjectUser User { get; set; }
    }
}
