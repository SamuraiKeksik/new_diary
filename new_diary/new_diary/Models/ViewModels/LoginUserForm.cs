﻿using System.ComponentModel.DataAnnotations;

namespace new_diary.Models.ViewModels
{
    public class LoginUserForm
    {        
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
