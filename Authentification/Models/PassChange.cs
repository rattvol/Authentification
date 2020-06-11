using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Models
{
    public class PassChange: User
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароль введен неверно")]
        [Display(Name = "Повторите пароль")]
        public string ConfirmNewPassword { get; set; }

    }
}
