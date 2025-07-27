using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSaveReader
{
    public class PokemonClass
    {
        protected readonly byte[] _saveFile;

        public PokemonClass(byte[] fileBytes) => _saveFile = fileBytes;
        public string Name { get; set; } = "No Pokemon";
        public int CurrentHP { get; set; } = 0;
        public int MaxHP { get; set; } = 0;
        public int Level { get; set; } = 0;
        public void PrintDetails()
        {
            Console.WriteLine($"The Pokemon's Name is {Name}");
            Console.WriteLine($"The Pokemon's level is {Level}");
            Console.WriteLine($"The Pokemon's current HP is {CurrentHP}");
            Console.WriteLine($"The Pokemon's Max HP is {MaxHP}");
            Console.WriteLine("");
        }
    }

    public class PokemonGenOne : PokemonClass
    {

        private int x = 1;
        public PokemonGenOne(byte[] fileBytes) : base(fileBytes) { }
        public void SetTemp(int i) => x = i;
        public int GetTemp() => x;

        // temp checkling
        
    }

}
