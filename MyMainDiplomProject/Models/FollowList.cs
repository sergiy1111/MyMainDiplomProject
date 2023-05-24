using MyMainDiplomProject.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MyMainDiplomProject.Models
{
    public class FollowList
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FolloverUserId { get; set; }

        public virtual MyMainDiplomProjectUser User1 { get; set; }
        public virtual MyMainDiplomProjectUser User2 { get; set; }
    }
}
