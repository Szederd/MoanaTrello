using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoanaTrello.Models.Helpers
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }

    public class LoginRequest
    {
        [Required(ErrorMessage = "Email cím megadása kötelező!")]
        [EmailAddress(ErrorMessage = "Hibás email cím!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Jelszó megadása kötelező!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    public class RegisterRequest
    {
        [Required(ErrorMessage = "Email cím megadása kötelező!")]
        [EmailAddress(ErrorMessage = "Hibás email cím!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Megerősítő email cím megadása kötelező!")]
        public string EmailConf { get; set; }
        [Required(ErrorMessage = "Jelszó megadása kötelező!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string PasswordConf { get; set; }
    }
}
