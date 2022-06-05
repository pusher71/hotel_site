using System.ComponentModel.DataAnnotations;

namespace hotel_site.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Логин не указан")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Пароль не указан")]
        [UIHint("password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Пароль не указан")]
        [UIHint("password")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Имя не указано")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия не указана")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Номер телефона не указан")]
        public string PhoneNumber { get; set; }

        [Required]
        public bool Agree { get; set; }
    }
}
