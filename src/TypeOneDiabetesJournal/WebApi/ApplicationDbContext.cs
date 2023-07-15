using DataLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public class ApplicationDbContext : sewright22_foodjournalContext
    {
        public ApplicationDbContext() : base()
        { }

        public ApplicationDbContext(DbContextOptions<sewright22_foodjournalContext> options) : base(options)
        { }
    }
}
