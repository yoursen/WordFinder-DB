using System.ComponentModel.DataAnnotations.Schema;
namespace DB;

public interface IGameWordCategory
{
    int Id { get; set; }
    string Name { get; set; }
}

[Table("GameWordCategories")]
public class GameWordCategory : IGameWordCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
}

[Table("GameWordCategories_uk_UA")]
public class GameWordCategory_uk_UA : IGameWordCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
}