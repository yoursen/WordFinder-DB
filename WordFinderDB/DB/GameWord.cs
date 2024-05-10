namespace DB;
public class GameWord
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