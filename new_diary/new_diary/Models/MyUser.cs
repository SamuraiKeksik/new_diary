namespace new_diary.Models
{
    public class MyUser
    {
        public int NotebookCount { get; set; } //кол-во блокнотов
        public int NoteCount { get; set; } // Кол-во записок
        public byte[] UserPicture { get; set; } //Картинка пользователя
    }
}
