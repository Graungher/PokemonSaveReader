using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSaveReader
{
    public abstract class PokemonGame
    {
        public abstract int PokedexIdFromByte(byte species);
        public abstract int FirstBoxOffset { get; }
        public abstract int BoxHeaderOffset { get; }
        public abstract int BoxSize { get; }
        public abstract int PokemonSize { get; }
        public abstract int NameOffset { get; }
        public abstract int LevelOffset { get; }
        public virtual int? ActiveBoxOffset => null;
        public virtual int? ActiveBoxBitOffset => null;
    }
    public class PokemonBlue : PokemonGame
    {
        private readonly int _boxOneOffset = 0x4000;        // box 1's memory Offset
        private readonly int _boxHeaderOffset = 0x16;       // the size of the box header
        private readonly int _boxSize = 0x462;              // the size of each box
        private readonly int _pokemonSize = 0x21;           // the size of the pokemon data struct
        private readonly int _activeBoxBitOffset = 0x284C;  // active box's identifer memory offset
        private readonly int _activeBoxOffset = 0x30C0;     // active box's memory Offset
        public override int PokedexIdFromByte(byte species)
        {
            return GenOneDictionary.ByteToPokedex[species];
        }
        public override int? ActiveBoxOffset => _activeBoxOffset;
        public override int? ActiveBoxBitOffset => _activeBoxBitOffset;
        public override int FirstBoxOffset => _boxOneOffset;
        public override int BoxHeaderOffset => _boxHeaderOffset;
        public override int BoxSize => _boxSize;
        public override int PokemonSize => _pokemonSize;
        public override int NameOffset => 0;
        public override int LevelOffset => 3; // still temporary
    }
    public class PokemonRed : PokemonBlue
    { }
}
