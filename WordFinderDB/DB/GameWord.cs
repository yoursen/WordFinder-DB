using System.ComponentModel.DataAnnotations.Schema;

namespace DB;

public interface IGameWord
{
    int Id { get; set; }
    string Word { get; set; }
    string Description { get; set; }
    bool IsPlayed { get; set; }
    bool IsAnswered { get; set; }
    bool IsPro { get; set; }
    //GameWordCategory Category { get; set; }
    ComplexityType Complexity { get; set; }
}


[Table("GameWords")]
public class GameWord : IGameWord
{
    public int Id { get; set; }
    public string Word { get; set; }
    public string Description { get; set; }
    public bool IsPlayed { get; set; }
    public bool IsAnswered { get; set; }
    public bool IsPro { get; set; }
    public GameWordCategory Category { get; set; }
    public ComplexityType Complexity { get; set; }
}

[Table("GameWords_uk_UA")]
public class GameWord_uk_UA : IGameWord
{
    public int Id { get; set; }
    public string Word { get; set; }
    public string Description { get; set; }
    public bool IsPlayed { get; set; }
    public bool IsAnswered { get; set; }
    public bool IsPro { get; set; }
    public GameWordCategory_uk_UA Category { get; set; }
    public ComplexityType Complexity { get; set; }
}