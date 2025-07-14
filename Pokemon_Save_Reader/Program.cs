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
                a.set_Game(0);
                a.go_to_box(1);
                a.go_to_pokemon(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }
    }
}