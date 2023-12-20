using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LivingDexOrganizer.Pokemon
{
    internal class LivingDex
    {
        private const int BoxSize = 30;

        [JsonPropertyName("$id")]
        public string ID { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string GameId { get; set; }
        public string Title { get; set; }
        public string OwnerId { get; set; }
        public string Format { get; set; }
        public string LegacyPresetId { get; set; }
        public int LegacyPresetVersion { get; set; }
        public List<Box> Boxes { get; set; }

        [JsonConstructor]
        public LivingDex(string id, DateTime creationTime, DateTime lastUpdateTime, string gameId, string title, string ownerId, string format, string legacyPresetId, int legacyPresetVersion, List<Box> boxes)
        {
            ID = id;
            CreationTime = creationTime;
            LastUpdateTime = lastUpdateTime;
            GameId = gameId;
            Title = title;
            OwnerId = ownerId;
            Format = format;
            LegacyPresetId = legacyPresetId;
            LegacyPresetVersion = legacyPresetVersion;
            Boxes = boxes;
        }

        public LivingDex(LivingDex oldDex, List<Pokedex> lists)
        {
            ID = oldDex.ID;
            CreationTime = oldDex.CreationTime;
            LastUpdateTime = oldDex.LastUpdateTime;
            GameId = oldDex.GameId;
            Title = oldDex.Title;
            OwnerId = oldDex.OwnerId;
            Format = oldDex.Format;
            LegacyPresetId = oldDex.LegacyPresetId;
            LegacyPresetVersion = oldDex.LegacyPresetVersion;

            CompareCaptured(ref lists, oldDex.GetAllPokemon());

            Boxes = CreateBoxes(lists);
        }


        public override string ToString()
        {
            return $"Boxes: {Boxes.Count}";
        }

        private List<Pokemon> GetAllPokemon()
        {
            List<Pokemon> pokemon = new();

            foreach (var box in Boxes)
            {
                pokemon.AddRange(box.Pokemon.OfType<Pokemon>().ToList());
            }

            return pokemon;
        }

        private static void CompareCaptured(ref List<Pokedex> dexes, List<Pokemon> existing)
        {
            Console.WriteLine($"Comparing existing mon.");

            foreach (Pokemon? existingMon in existing)
            {
                if (existingMon == null)
                {
                    continue;
                }

                foreach (var list in dexes)
                {
                    Pokemon? poke = list.Pokemon.FirstOrDefault(p => p != null && p.Id == existingMon.Id);

                    if (poke != null && existingMon.Captured && !poke.Shiny)
                    {
                        poke.Captured = existingMon.Captured;
                    }
                }
            }
        }

        private static List<Box> CreateBoxes(List<Pokedex> dexes)
        {
            List<Box> boxes = new();
            int boxCount = 0;
            int dexIndex = 0;
            int pokeIndex = 0;
            int boxPage = 0;

            Console.WriteLine($"Creationg boxes.");

            foreach (var dex in dexes)
            {
                boxCount += dex.Pokemon.Count;
            }

            boxCount = (int)Math.Ceiling((float)boxCount / BoxSize);

            Console.WriteLine($"Living dex will have {boxCount} boxes.");

            for (int i = 0; i <= boxCount; i++)
            {
                boxPage++;
                Box box = new($"{dexes[dexIndex].Name} {boxPage}", false);

                Console.WriteLine($"Adding box page {boxPage} of dex {dexes[dexIndex].Name}.");

                for (int j = 0; j < BoxSize; j++)
                {
                    if (pokeIndex == dexes[dexIndex].Pokemon.Count)
                    {
                        box.Pokemon.Add(null);
                        continue;
                    }

                    box.Pokemon.Add(dexes[dexIndex].Pokemon[pokeIndex]);

                    Console.WriteLine($"Adding pokemon {dexes[dexIndex].Pokemon[pokeIndex].Id} to page {boxPage}.");

                    pokeIndex++;
                }

                boxes.Add(box);

                if (pokeIndex == dexes[dexIndex].Pokemon.Count)
                {
                    dexIndex++;
                    pokeIndex = 0;
                    boxPage = 0;

                    if (dexIndex == dexes.Count)
                    {
                        break;
                    }
                }
            }

            return boxes;
        }
    }
}
