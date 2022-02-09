using OnAuction.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnAuction.Models
{
    public class LotCreateModel
    {
        [Required(ErrorMessage = "Необхідно вказати назву")]
        [DisplayName("Назва")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Необхідно вказати опис")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Опис")]
        public string Description { get; set; }
        [DisplayName("Зображення")]
        [FileSize(16, ErrorMessage = "Розмір зображення має бути менше 16 Мб")]
        [FileType(".png,.jpg,.jpeg,.bmp,.gif", ErrorMessage = "Зображення не відповідає формату (*.png, *.jpg, *.jpeg, *.bmp, *.gif)")]
        public HttpPostedFileBase Image { get; set; }
        [Required(ErrorMessage = "Необхідно вказати стартову вартість")]
        [Range(1, int.MaxValue, ErrorMessage = "Стартова вартість має бути не менше ніж 1 грн.")]
        [DisplayName("Стартова вартість, грн.")]
        public int StartPrice { get; set; }
        [Required(ErrorMessage = "Необхідно вказати вартість для моментального викупу")]
        [Range(1, int.MaxValue, ErrorMessage = "Вартість для моментального викупу має бути не менше ніж 1 грн.")]
        [DisplayName("Вартість для моментального викупу, грн.")]
        public int InstantPrice { get; set; }
        [Required(ErrorMessage = "Необхідно вказати крок ставки")]
        [Range(1, int.MaxValue, ErrorMessage = "Крок ставки має бути не менше ніж 1 грн.")]
        [DisplayName("Крок ставки, грн.")]
        public int BetStep { get; set; }
        [Required(ErrorMessage = "Необхідно вказати категорію лоту")]
        [DisplayName("Категорія")]
        public int CategoryId { get; set; }
    }
}
