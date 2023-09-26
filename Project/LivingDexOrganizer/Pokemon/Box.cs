using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LivingDexOrganizer.Pokemon
{
    internal class Box
    {
        public string Title { get; set; }
        public bool Shiny { get; set; }
        public List<Pokemon?> Pokemon { get; set; }

        [JsonConstructor]
        public Box(string title, bool shiny, List<Pokemon?> pokemon)
        {
            Title = title;
            Shiny = shiny;
            Pokemon = pokemon;
        }

        public Box(string title, bool shiny)
        {
            Title = title;
            Shiny = shiny;
            Pokemon = new();
        }

        public override string ToString()
        {
            return $"{Title}: {Pokemon.Count}";
        }
    }
}
