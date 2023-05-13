using MyMainDiplomProject.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MyMainDiplomProject.Models
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual MyMainDiplomProjectUser User { get; set; }
    }
}
