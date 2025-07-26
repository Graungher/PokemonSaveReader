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
                
                int i;
                int boxNo;
                int monNo;
                bool quit = false;
                while (!quit)
                {
                    Console.WriteLine("Whatcha wanna do? 1: Read Box   2: Read them all?   3: Look at a guy?   4: Quit?");
                    int.TryParse(Console.ReadLine(), out i);

                    switch (i)
                    {
                        case 1:
                            Console.WriteLine("Gimme a box number:");
                            int.TryParse(Console.ReadLine(), out boxNo);

                            ReadBox(boxNo, a);
                            break;
                        case 2:
                            ReadAllBoxes(a);
                            break;
                        case 3:
                            Console.WriteLine("Gimme a box number:");
                            int.TryParse(Console.ReadLine(), out boxNo);

                            Console.WriteLine("Gimme a man number:");
                            int.TryParse(Console.ReadLine(), out monNo);

                            ReadPokemonInBox(monNo, boxNo, a);
                            break;
                        default:
                            quit = true;
                            return;
                    }
                }

                // here add option for selecting a specific pokemon then show all details
                // prob select box
                // prob select option
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }
        static void ReadPokemonInBox(int pokemonIndex, int boxNumber, PokemonBoxHandler a)
        {
            String pokemonName;
            if ((pokemonIndex! > a.BoxOccupantCount(boxNumber)) && pokemonIndex > 0)
            {
                Console.WriteLine("Uh Oh, cant do that bucco!");
                return;
            }
            pokemonName = a.GetPokemonNameFromBox(boxNumber, pokemonIndex);
            Console.WriteLine($"{pokemonIndex,2}: {pokemonName}");
        }
        static void ReadBox(int boxNumber, PokemonBoxHandler a)
        {
            int pokemonCount = a.BoxOccupantCount(boxNumber);

            if (pokemonCount != 0)
            {
                Console.WriteLine($"\nBox: {boxNumber}");
                for (int pokemonIndex = 1; pokemonIndex <= pokemonCount; pokemonIndex++)
                {
                    ReadPokemonInBox(pokemonIndex, boxNumber, a);
                }
            }
        }
        static void ReadAllBoxes(PokemonBoxHandler a)
        {
            int totalBoxes = a.MaxBoxes();
            Console.Write("This is every Pokemon in the storage");
            for (int boxNumber = 1; boxNumber <= totalBoxes; boxNumber++)
            {
                ReadBox(boxNumber, a);
            }
        }
    }
}