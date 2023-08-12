using Microsoft.EntityFrameworkCore;

namespace PersonaClassLibrary;

public class JournalDBContext : DbContext
{
    public JournalDBContext(DbContextOptions<JournalDBContext> options)
        : base(options)
    {
    }
    public DbSet<JournalModel> JournalList { get; set; } = null!;
}