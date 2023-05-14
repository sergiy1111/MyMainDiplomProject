using MyMainDiplomProject.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MyMainDiplomProject.Models
{
    public class UserAdditionalInfo
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? Description { get; set; }
        public bool ShowDescription { get; set; }
        public string? WorkPlase { get; set; }
        public bool ShowWorkPlase { get; set; }
        public string? UserInterests { get; set; }
        public bool ShowUserInterests { get; set; }
        public string? Education { get; set; }
        public bool ShowEducation { get; set; }
        public string? UserAvatar { get; set; }

        public virtual MyMainDiplomProjectUser User { get; set; }
    }
}
