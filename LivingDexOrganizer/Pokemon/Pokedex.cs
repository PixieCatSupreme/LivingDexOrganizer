using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivingDexOrganizer.Pokemon
{
    internal class Pokedex
    {
        public string Name { get; set; }
        public List<Pokemon> Pokemon { get; set; }

        public Pokedex(string fileName) 
        {
            Name = Path.GetFileNameWithoutExtension(fileName);

            Pokemon = GetPokemonList(Path.Combine("./Dexes",fileName));
        }

        public void AddRange(List<Pokemon> pokemon)
        {
            Pokemon.AddRange(pokemon);
        }

        public override string ToString()
        {
            return $"{Name}: {Pokemon.Count}";
        }

        private static List<Pokemon> GetPokemonList(string path)
        {
            List<Pokemon> pokemon = new();

            using (StreamReader fs = new(path))
            {
                while (!fs.EndOfStream)
                {
                    string? id = fs.ReadLine();

                    if (!string.IsNullOrWhiteSpace(id) && !id.StartsWith("//"))
                    {
                        pokemon.Add(new(id.ToLower()));
                    }
                }
            }

            return pokemon;
        }
    }
}
