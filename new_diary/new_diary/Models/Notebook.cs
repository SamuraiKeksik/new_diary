using System.ComponentModel.DataAnnotations;

namespace new_diary.Models
{
    public class Notebook
    {
        [Key]
        public Guid NotebookId { get; set; } = Guid.NewGuid(); //Id блокнота
        public Guid UserId { get; set; }
        public string NotebookName { get; set;} //Название блокнота
        public int NoteCount { get; set; } //Кол-во записок в блокноте
    }
}
