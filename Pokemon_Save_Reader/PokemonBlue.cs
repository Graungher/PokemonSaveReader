using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSaveReader
{
    public class PokemonBlue : IPokemonGame
    {
        private readonly int _boxOneOffset = 0x4000;        // box 1's memory Offset
        private readonly int _boxHeaderOffset = 0x16;     // the size of the box header
        private readonly int _boxSize = 0x462;             // the size of each box
        private readonly int _pokemonSize = 0x21;          // the size of the pokemon data struct

        public int PokedexIdFromByte(byte species)
        {
            return GenOneDictionary.ByteToPokedex[species];
        }
//        public int GetActiveBoxIndex()
//        {
//            return _saveFile[0x284C] & 0x0F; // zero-based index (0–11)
//        }
        public int FirstBoxOffset => _boxOneOffset;
        public int BoxHeaderOffset => _boxHeaderOffset;
        public int BoxSize => _boxSize;
        public int PokemonSize => _pokemonSize;
        public int NameOffset => 0;
        public int LevelOffset => 3; // still temporary
    }
}
