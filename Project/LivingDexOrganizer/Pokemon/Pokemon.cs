using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LivingDexOrganizer.Pokemon
{
    internal class Pokemon
    {
        public string Id { get; set; }
        public bool Captured { get; set; }
        public bool Shiny { get; set; }

        [JsonPropertyName("evs")]
        public List<int> EVs { get; set; }
        [JsonPropertyName("ivs")]
        public List<int> IVs { get; set; }
        public List<int> Moves { get; set; }
        public List<int> EmblemMarks { get; set; }

        [JsonConstructor]
        public Pokemon(string id, bool captured, bool shiny, List<int> eVs, List<int> iVs, List<int> moves, List<int> emblemMarks)
        {
            Id = id;
            Captured = captured;
            Shiny = shiny;
            EVs = eVs;
            IVs = iVs;
            Moves = moves;
            EmblemMarks = emblemMarks;
        }

        public Pokemon(string id)
        {
            Id = id;

            Captured = false;
            Shiny = false;

            EVs = new List<int>();
            IVs = new List<int>();
            Moves = new List<int>();
            EmblemMarks = new List<int>();
        }

        public Pokemon(Pokemon other)
        {
            Id = other.Id;

            Captured = other.Captured;
            Shiny = other.Captured;

            EVs = new List<int>();
            IVs = new List<int>();
            Moves = new List<int>();
            EmblemMarks = new List<int>();
        }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}
