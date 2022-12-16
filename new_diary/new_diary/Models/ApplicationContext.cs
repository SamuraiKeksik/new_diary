using Microsoft.EntityFrameworkCore;

namespace new_diary.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Notebook> Notebooks { get; set; }
        public DbSet<MyUser> Users { get; set; } 

        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options) 
        {
            Database.EnsureCreated(); //Создание БД при первом обращении
        }
        
    }
}
