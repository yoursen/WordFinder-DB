using System.ComponentModel.DataAnnotations.Schema;
namespace DB;
[Table("GameWordCategories")]
public class GameWordCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
}