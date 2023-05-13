using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MyMainDiplomProject.Models
{
    public class HashTags
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Post>? Post { get; set; }
    }
}
