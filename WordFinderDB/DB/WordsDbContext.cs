using Microsoft.EntityFrameworkCore;
namespace DB;
public class WordsDbContext : DbContext
{
    public DbSet<GameWord> GameWords { get; set; }
    public DbSet<GameWordCategory> GameWordCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=DB/Words.db");
    }
}