using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MyMainDiplomProject.Models
{
    public class Files
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public string FileName { get; set; }
        public virtual Post Post { get; set; }
    }
}
