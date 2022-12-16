using System.ComponentModel.DataAnnotations.Schema;

namespace new_diary.Models
{
    public class Note
    {
        public Guid Id { get; set; } //Id записки
        public Guid UserId { get; set; } //Id пользователя
        public Guid NotebookId { get; set; } //Id блокнота
        public string Title { get; set; } = string.Empty; //Заголовок записки

        [Column(TypeName = "varchar(MAX)")] //В sql поле хранит 2000 символов
        public string Text { get; set; } = string.Empty; //Текст записки
    }
}
