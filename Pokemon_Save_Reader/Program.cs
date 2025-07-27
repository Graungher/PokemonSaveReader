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
                UserInterface(a);

                //PokemonClass guy = a.GetPokemonFromBox(1, 3);
                //guy.PrintDetails();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }

        // -------------------------------
        // UI - TECHNICALLY
        // -------------------------------
        static void UserInterface(PokemonBoxHandler a)
        {
            int i;
            int boxNo;
            int monNo;
            bool quit = false;

            while (!quit)
            {
                Console.WriteLine("Whatcha wanna do fam? 1: Read Box   2: Read them all?   3: Look at a guy?   4: Quit?");
                int.TryParse(Console.ReadLine(), out i);
                //////////////////////////////
                quit = true;
                //////////////////////////////
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

                        GetPokemonFromBox(boxNo, monNo, a);
                        break;
                    default:
                        quit = true;
                        return;
                }
            }
        }

        // -------------------------------
        // POKEMON HANDLING
        // -------------------------------
        static void GetPokemonFromBox(int boxNumber, int pokemonIndex, PokemonBoxHandler a)
        {
            PokemonClass pokemon;
            if ((boxNumber > a.MaxBoxes()) || boxNumber <= 0)
            {
                Console.WriteLine("Thats not even a valid box BOZO!");
                return;
            }
            if ((pokemonIndex > a.BoxOccupantCount(boxNumber)) || pokemonIndex <= 0)
            {
                Console.WriteLine("Thats an invalid pokemon index Bucco!");
                return;
            }

            pokemon = a.GetPokemonFromBox(boxNumber, pokemonIndex);
            pokemon.PrintDetails();
        }
        static void ReadPokemonNameInBox(int boxNumber, int pokemonIndex, PokemonBoxHandler a)
        {
            String pokemonName;
            if ((boxNumber > a.MaxBoxes()) || boxNumber <= 0)
            {
                Console.WriteLine("Thats not even a valid box BOZO!");
                return;
            }
            if ((pokemonIndex > a.BoxOccupantCount(boxNumber)) || pokemonIndex <= 0)
            {
                Console.WriteLine("Thats an invalid pokemon index Bucco!");
                return;
            }
            pokemonName = a.GetPokemonNameFromBox(boxNumber, pokemonIndex);
            Console.WriteLine($"{pokemonIndex,2}: {pokemonName}");
        }

        // -------------------------------
        // BOX HANDLING
        // -------------------------------
        static void ReadBox(int boxNumber, PokemonBoxHandler a)
        {
            if ((boxNumber > a.MaxBoxes()) || boxNumber <= 0)
            {
                Console.WriteLine("Thats not even a valid box BOZO!");
                return;
            }

            int pokemonCount = a.BoxOccupantCount(boxNumber);

            if (pokemonCount != 0)
            {
                Console.WriteLine($"\nBox: {boxNumber}");
                for (int pokemonIndex = 1; pokemonIndex <= pokemonCount; pokemonIndex++)
                {
                    ReadPokemonNameInBox(boxNumber, pokemonIndex, a);
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