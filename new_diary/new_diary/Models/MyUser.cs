using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace new_diary.Models
{
    public class MyUser : IdentityUser       
    {
        public int NotebookCount { get; set; } = 0; //кол-во блокнотов
        public int NoteCount { get; set; } = 0;// Кол-во записок
        public byte[] UserPicture { get; set; } //Картинка пользователя
    }
}
