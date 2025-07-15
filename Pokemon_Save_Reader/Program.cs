using Pokemon_Save_Reader;
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

                Pokemon_Box_Handler a = new Pokemon_Box_Handler(fileBytes);
                a.SetGame(Pokemon_Box_Handler.GameVersion.Blue);
                a.GoToBox(1);
                a.GoToPokemon(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }
    }
}