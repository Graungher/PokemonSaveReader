using PokemonSaveReader;
using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "Pokemon.sav";
            byte[] fileBytes;

            try
            {
                fileBytes = File.ReadAllBytes(filePath);

                PokemonBoxHandler a = new PokemonBoxHandler(fileBytes, GameVersion.Blue);
                a.GoToBox(2);
                a.GoToPokemon(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }
    }
}