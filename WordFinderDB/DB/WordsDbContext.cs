using Microsoft.EntityFrameworkCore;
namespace DB;
public class WordsDbContext : DbContext
{
    public DbSet<GameWord> GameWords { get; set; }
    public DbSet<GameWordCategory> GameWordCategories { get; set; }

    public DbSet<GameWord_uk_UA> GameWords_uk_UA { get; set; }

    public DbSet<GameWordCategory_uk_UA> GameWordCategories_uk_UA { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=DB/Words.db");

    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<GameWord>().ToTable("GameWords");
    //     modelBuilder.Entity<GameWord_uk_UA>().ToTable("GameWords_uk_UA");

    //     modelBuilder.Entity<GameWordCategory>().ToTable("GameWordCategories");
    //     modelBuilder.Entity<GameWordCategory_uk_UA>().ToTable("GameWordCategories_uk_UA");
    // }
}