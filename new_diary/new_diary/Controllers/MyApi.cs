using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using new_diary.Models;

namespace new_diary.Controllers
{
    public class MyApi : Controller
    {
        private readonly ApplicationContext _dbContext;
        private readonly UserManager<MyUser> _userManager;
        public MyApi(ApplicationContext dbContext, UserManager<MyUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ActionResult> GetNote(string NoteId)
        {
            var note = await _dbContext.FindAsync<Note>(NoteId);
            return Json(new { note.Id, note.Title, note.Text });
        }

        public async Task<bool> UpdateNote(string NoteId, string title, string text)
        {
            var note = await _dbContext.FindAsync<Note>(NoteId);
            if (note == null)
                return false; 
            note.Title = title;
            note.Text = text;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
