using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace new_diary.Models
{
    public class IdentityContext : IdentityDbContext<MyUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {

    }
}
}
