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
                //PokemonClass pokemon;
                //String pokemonName;

                PokemonBoxHandler a = new PokemonBoxHandler(fileBytes, GameVersion.Blue);
                readAllBoxes(a);


                // here add option for selecting a specific pokemon then show all details
                // prob select box
                // prob select option
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }

        static void readBox(int boxNumber, PokemonBoxHandler a)
        {
            int pokemonCount = a.BoxOccupantCount(boxNumber);
            String pokemonName;

            if (pokemonCount != 0)
            {
                Console.WriteLine($"\nBox: {boxNumber}");
                for (int pokemonIndex = 1; pokemonIndex <= pokemonCount; pokemonIndex++)
                {
                    pokemonName = a.GetPokemonNameFromBox(boxNumber, pokemonIndex);
                    Console.WriteLine($"{pokemonIndex,2}: {pokemonName}");
                }
            }
        }
        static void readAllBoxes(PokemonBoxHandler a)
        {
            int totalBoxes = a.MaxBoxes();
            Console.Write("This is every Pokemon in the storage");
            for (int boxNumber = 1; boxNumber <= totalBoxes; boxNumber++)
            {
                readBox(boxNumber, a);
            }
        }
    }
}