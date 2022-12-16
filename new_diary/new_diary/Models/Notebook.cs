namespace new_diary.Models
{
    public class Notebook
    {
        public Guid NotebookId { get; set; } //Id блокнота
        public string NotebookName { get; set;} //Название блокнота
        public int NoteCount { get; set; } //Кол-во записок в блокноте
    }
}
