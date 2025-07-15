using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Save_Reader
{
    public class Pokemon_Red : IPokemonGame
    {
        private readonly int _box_1_offset = 0x4000;
        private readonly int _box_header_offset = 0x16;
        private readonly int _box_size = 0x462;
        private readonly int _pokemon_size = 0x21;

        public int getSpecies(byte species)
        {
            return Gen_1_Dictionary.Byte_to_Pokedex[species];
        }
        public int get_box_1_offset()
        {
            return _box_1_offset;
        }
        public int get_box_header_offset()
        {
            return _box_header_offset;
        }
        public int get_box_size()
        {
            return _box_size;
        }
        public int get_pokemon_size()
        {
            return _pokemon_size;
        }
    }
}
