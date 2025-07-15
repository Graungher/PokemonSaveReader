using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Save_Reader
{
    public class Pokemon_Blue : IPokemonGame
    {
        private readonly int _box_1_offset = 0x4000;        // box 1's memory Offset
        private readonly int _box_header_offset = 0x16;     // the size of the box header
        private readonly int _box_size = 0x462;             // the size of each box
        private readonly int _pokemon_size = 0x21;          // the size of the pokemon data struct

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
        public int get_name_offset()
        {
            return 0;
        }
        public int get_level_offset()
        {
            return 1; // temp func
        }

    }
}
