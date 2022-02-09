using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnAuction.Models
{
    public class RegisterModel
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

        [Required(ErrorMessage = "Необхідно підтвердити пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Введені паролі не співпадають")]
        [DisplayName("Підтвердження паролю")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Необхідно вказати електронну пошту")]
        [EmailAddress(ErrorMessage = "Некоректна електронна пошта")]
        [DisplayName("Електронна пошта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Необхідно вказати номер телефону")]
        [RegularExpression(@"380[0-9]{9}", ErrorMessage = "Некоректний номер телефону. Формат: 380xxxxxxxxx")]
        [DisplayName("Номер телефону")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Продавець")]
        public bool IsSeller { get; set; }
    }
}
