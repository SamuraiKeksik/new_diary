using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using new_diary.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

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

        public async Task<ActionResult> GetNote(string noteId)
        {
            var note = await _dbContext.Notes.FindAsync(new Guid(noteId));
            if(note == null)
                return Json(noteId);
            return Json(new {note.Text });
        }

        public class PutNote //класс для UpdateNote()
        {
            public string id { get; set; }
            public string text { get; set; }
        }
        public async Task<bool> UpdateNote([FromBody] PutNote putNote)
        {
            var noteId = putNote.id;
            var note = await _dbContext.Notes.FindAsync(new Guid(noteId));
            if (note == null)
                return false; 
            //note.Title = ;
            note.Text = putNote.text;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
