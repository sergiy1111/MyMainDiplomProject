using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MyMainDiplomProject.Models.ViewModels
{
    public class ChangeSeatingsViewModel
    {
        public int Id { get; set; }
        public UserAdditionalInfo UserAdditional { get; set; }

        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}
