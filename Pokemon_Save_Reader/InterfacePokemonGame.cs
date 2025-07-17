using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSaveReader
{
    public interface IPokemonGame
    {
        int PokedexIdFromByte(byte species);
        int FirstBoxOffset { get; }
        int BoxHeaderOffset { get; }
        int BoxSize { get; }
        int PokemonSize { get; }
        int NameOffset { get; }
        int LevelOffset { get; }
    }
}
