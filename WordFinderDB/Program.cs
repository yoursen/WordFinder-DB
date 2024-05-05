using System.Text.Json;
using DB;
using Json;

class Program
{
    static async Task Main(string[] args)
    {
        var words = GetWords();
        var categories = words.Select(w => w.category).Distinct().ToList();

        var dbFile = Directory.GetCurrentDirectory() + @"/DB/Words.db";
        if (File.Exists(dbFile))
            File.Delete(dbFile);

        using (var db = new WordsDbContext())
        {
            db.Database.EnsureCreated();
            db.GameWords.RemoveRange(db.GameWords.ToArray());

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
        nameof(ComplexityType.Hard) => ComplexityType.Hard,
        nameof(ComplexityType.Medium) => ComplexityType.Medium,
        nameof(ComplexityType.Easy) => ComplexityType.Easy,
        _ => throw new ArgumentException()
    };

    private static WordJson[] GetWords()
    {
        List<WordJson> words = new();
        var path = Directory.GetCurrentDirectory();
        foreach (var f in Directory.EnumerateFiles(path + "/Json", "*.json"))
        {
            var context = File.ReadAllText(f);
            var jsonWords = JsonSerializer.Deserialize<WordJson[]>(context);
            words.AddRange(jsonWords);
            Console.Write($"Read {jsonWords.Count()} words from file {Path.GetFileName(f)}");
        }
        Console.WriteLine($"Total words count {words.Count}");
        return words.ToArray();
    }
}