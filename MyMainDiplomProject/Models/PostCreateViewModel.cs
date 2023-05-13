using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyMainDiplomProject.Models
{
    public class PostCreateViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [Display(Name = "Текст посту")]
        [Required(ErrorMessage = "Поле має бути заповненим")]
        public string Text { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string CreaterDateRime { get; set; }

        [Display(Name = "Файли")]
        public List<Files> Files { get; set; }

        [Display(Name = "Хештеги")]
        public List<PostHashTags> PostHashTags { get; set; }
    }
}
