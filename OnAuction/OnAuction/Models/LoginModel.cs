using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnAuction.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Необхідно вказати логін")]
        [RegularExpression(@"[A-Za-z0-9_]{3,16}", ErrorMessage = "Некоректний логін")]
        [DisplayName("Логін")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Необхідно вказати пароль")]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Довжина паролю має бути від 8 до 32 символів")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
