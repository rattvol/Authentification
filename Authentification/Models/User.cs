using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Authentification.Models
{
    public partial class User : PublicUser
    {
       
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [RegularExpression(@"(?=.*([a-zA-Zа-яА-ЯёЁ]+).*(\d+).*).{8,}", ErrorMessage = "Пароль должен содержать не менее 8 символов, должны присутствовать буквы и цифры")]
        public string Password { get; set; }

        
    }
}
