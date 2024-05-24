using System.Text.Json;
using System.Text.RegularExpressions;
using DB;
using Json;

class Program
{
    static async Task Main(string[] args)
    {
        var dbFile = Directory.GetCurrentDirectory() + @"/DB/Words.db";
        if (File.Exists(dbFile))
            File.Delete(dbFile);

        await FillDB<GameWord, GameWordCategory>("", @"[^a-zA-Z]");
        await FillDB<GameWord_uk_UA, GameWordCategory_uk_UA>("_uk_UA", @"[^a-zA-Zа-яА-ЯіїІЇґҐєЄʼ']");
    }

    private static async Task FillDB<TWords, TCategory>(string suffix, string regexFilter)
        where TWords : class, IGameWord
        where TCategory : class, IGameWordCategory
    {
        var words = GetWords(suffix);
        var categories = words.Select(w => w.category).Distinct().ToList();

        using (var db = new WordsDbContext())
        {
            db.Database.EnsureCreated();
            db.Set<TWords>().RemoveRange(db.Set<TWords>().ToArray());

            foreach (var category in categories)
            {
                if (db.Set<TCategory>().Any(c => c.Name == category))
                    continue;

                var cat = Activator.CreateInstance<TCategory>();
                cat.Name = category;
                db.Set<TCategory>().Add(cat);
            }

            await db.SaveChangesAsync();

            foreach (var word in words)
            {
                if (Regex.IsMatch(word.word, regexFilter) || db.Set<TWords>().Any(w => w.Word == word.word))
                {
                    Console.WriteLine($"-->{word.word}<--");
                    continue;
                }

                var category = db.Set<TCategory>().First(c => c.Name == word.category);
                var w = Activator.CreateInstance<TWords>();
                w.Word = word.word;
                w.Description = word.description;
                
                dynamic d = w;
                d.Category = category;
                
                w.IsPro = !(word.isPro == "False" || word.isPro == "false");
                w.Complexity = GetComplexity(word.complexity);

                db.Set<TWords>().Add(w);
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

    private static WordJson[] GetWords(string suffix)
    {
        List<WordJson> words = new();
        var path = Directory.GetCurrentDirectory();
        foreach (var f in Directory.EnumerateFiles(path + "/Json" + suffix, "*.json"))
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