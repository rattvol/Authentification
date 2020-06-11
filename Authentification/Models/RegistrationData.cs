using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Models
{
    public class RegistrationData : User
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        [Display(Name = "Повторите пароль")]
        public string ConfirmPassword { get; set; }


    }
}
