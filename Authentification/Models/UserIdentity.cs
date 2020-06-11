using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Authentification.Models
{
    public class UserIdentity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        public string Token { get; set; }

    }
}
