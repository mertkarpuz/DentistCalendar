using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentistCalendar.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Lütfen kullanıcı adınızı belirtiniz.")]
        [Display(Name="Kullanıcı Adınız:")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen adınızı belirtiniz.")]
        [Display(Name = "Adınız:")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen soyadınızı belirtiniz.")]
        [Display(Name = "Soyadınız:")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Lütfen şifrenizi belirtiniz.")]
        [Display(Name = "Şifreniz:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Lütfen email adresinizi belirtiniz.")]
        [Display(Name = "Email Adresiniz:")]
        [EmailAddress(ErrorMessage ="Lütfen e mail alanını kontrol ediniz.")]
        public string Email { get; set; }


        [Display(Name = "Randevu Rengi:")]
        public string Color { get; set; }
        [Display(Name = "Doktorum")]
        public bool IsDentist { get; set; }

    }
}
