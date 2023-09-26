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

            List<Pokedex> lists = new()
            {
                new("./Paldea.txt"),
                new("./Kitakami.txt")
            };

            LivingDex newDex = new(oldDex, lists);

            string jsonString = JsonSerializer.Serialize(newDex, options);

            using (StreamWriter fs = new("./NewDex.json"))
            {
                fs.Write(jsonString);
            }
        }
    }
}