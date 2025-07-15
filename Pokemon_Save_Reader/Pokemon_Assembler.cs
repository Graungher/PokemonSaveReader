using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Save_Reader
{
    public abstract class Pokemon_Assembler
    {
        protected readonly byte[] _save_file;
        protected String _Name = string.Empty;
        protected int _Level = 1;

        public Pokemon_Assembler(byte[] fileBytes) => _save_file = fileBytes;
        public void setName(String inputName) => _Name = inputName;
        public String getName() => _Name;
        public void setLevel(int inputLevel) => _Level = inputLevel;
        public int getLevel() => _Level;
        
    }

    public class Gen1Assembler : Pokemon_Assembler
    {
        private int x = 1;
        public Gen1Assembler(byte[] fileBytes) : base(fileBytes) { }
        public void SetTemp(int i) => x = i;
        public int getTemp() => x;

        // temp checkling
        public void printDetails()
        {
            Console.WriteLine($"The Pokemon's Name is {_Name}");
        }
    }
}
