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

            LivingDex? oldDex = JsonSerializer.Deserialize<LivingDex>(fileString, options);

            if (oldDex == null)
            {
                return;
            }

            List<string> files = Directory.GetFiles(".\\Dexes").OrderBy(f => f).ToList();

            List<Pokedex> lists = new();

            foreach (var file in files)
            {
                lists.Add(new(file));
            }

            LivingDex newDex = new(oldDex, lists);

            string jsonString = JsonSerializer.Serialize(newDex, options);

            using (StreamWriter fs = new("./NewDex.json"))
            {
                fs.Write(jsonString);
            }
        }
    }
}