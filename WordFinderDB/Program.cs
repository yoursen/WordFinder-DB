using System.Text.Json;
using DB;
using Json;

class Program
{
    static async Task Main(string[] args)
    {
        var words = GetWords();
        var categories = words.Select(w => w.category).Distinct().ToList();
        using (var db = new WordsDbContext())
        {
            db.Database.EnsureCreated();

            foreach (var category in categories)
            {
                if (db.GameWordCategories.Any(c => c.Name == category))
                    continue;
                db.GameWordCategories.Add(new GameWordCategory() { Name = category });
            }

            await db.SaveChangesAsync();

            foreach (var word in words)
            {
                if (word.word.Contains(" ")
                 || word.word.Contains("'")
                 || db.GameWords.Any(w => w.Word == word.word))
                {
                    continue;
                }
                var category = db.GameWordCategories.First(c => c.Name == word.category);
                db.GameWords.Add(new GameWord()
                {
                    Word = word.word,
                    Description = word.description,
                    Category = category,
                    Complexity = GetComplexity(word.complexity)
                });
            }

            await db.SaveChangesAsync();
        }
    }

    private static ComplexityType GetComplexity(string complexity) => complexity switch
    {
        nameof(ComplexityType.Difficult) => ComplexityType.Difficult,
        nameof(ComplexityType.Moderate) => ComplexityType.Moderate,
        nameof(ComplexityType.Easy) => ComplexityType.Easy,
        _ => throw new ArgumentException()
    };

    private static WordJson[] GetWords()
    {
        var context = File.ReadAllText("Json/Words.json");
        var words = JsonSerializer.Deserialize<WordJson[]>(context);
        return words;
    }
}