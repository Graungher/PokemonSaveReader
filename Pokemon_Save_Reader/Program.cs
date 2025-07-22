using PokemonSaveReader;
using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "Pokemon3.sav";
            byte[] fileBytes;

            try
            {
                fileBytes = File.ReadAllBytes(filePath);
                PokemonClass pokemon;

                PokemonBoxHandler a = new PokemonBoxHandler(fileBytes, GameVersion.Blue);
                pokemon = a.GetPokemonFromBox(2, 17);
                if (pokemon is PokemonGenOne gen1)
                {
                    gen1.PrintDetails();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }
    }
}