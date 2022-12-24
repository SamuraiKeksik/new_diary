namespace new_diary.Models.ViewModels
{
    public class MainModel
    {
        public IEnumerable<Notebook> notebooks { get; set; }
        public IEnumerable<Note> notes { get; set; }
    }
}
