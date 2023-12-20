using System.Text.Json;
using LivingDexOrganizer.Pokemon;

namespace LivingDexOrganizer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileString = "";
            using (StreamReader fs = new("./OldDex.json"))
            {
                fileString = fs.ReadToEnd();
            }

            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            Console.WriteLine($"Reading old dex at './OldDex.json'.");

            LivingDex? oldDex = JsonSerializer.Deserialize<LivingDex>(fileString, options);

            if (oldDex == null)
            {
                Console.WriteLine($"Unable to serialize old dex.");
                return;
            }

            Console.WriteLine($"Getting living dex files.");

            List<string> files = Directory.GetFiles(".\\Dexes").OrderBy(f => f).ToList();

            List<Pokedex> lists = new();

            foreach (var file in files)
            {
                Console.WriteLine($"Adding dex file {file}.");
                lists.Add(new(file));
            }

            LivingDex newDex = new(oldDex, lists);

            string jsonString = JsonSerializer.Serialize(newDex, options);

            Console.WriteLine($"Writing to new dex file at './NewDex.json'.");

            using (StreamWriter fs = new("./NewDex.json"))
            {
                fs.Write(jsonString);
            }
        }
    }
}