﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSaveReader
{
    public abstract class PokemonGame
    {
        // -------------------------------
        // BOX STUFF
        // -------------------------------
        public virtual int? ActiveBoxBitOffset => null;
        public abstract int FirstBoxOffset { get; }
        public virtual int? ActiveBoxOffset => null;
        public abstract int BoxOccupancyOffset { get; }
        public abstract int BoxHeaderSize { get; }
        public abstract int BoxSize { get; }
        public abstract int MaxBoxes { get; }

        // -------------------------------
        // POKEMON STUFF
        // -------------------------------
        public abstract int PokemonCount { get; }
        public abstract int PokemonSize { get; }
        public abstract int NameOffset { get; }
        public abstract int LevelOffset { get; }

        // -------------------------------
        // UTILITIES
        // -------------------------------
        public abstract int PokedexIdFromByte(byte species);
    }

    public class PokemonBlue : PokemonGame
    {
        // -------------------------------
        // BOX STUFF
        // -------------------------------

        private const int _activeBoxBitOffset = 0x284C;
        private const int _boxOneOffset = 0x4000;
        private const int _activeBoxOffset = 0x30C0;
        private const int _boxPokemonCountOffset = 0x00;
        private const int _boxHeaderSize = 0x16;
        private const int _boxSize = 0x462;
        private const int _maxBoxes = 12;
        private const int _boxOccupancyOffset = 0x00;

        // -------------------------------
        // POKEMON STUFF
        // -------------------------------

        private const int _pokemonSize = 0x21;
        private const int _nameOffset = 0;
        private const int _levelOffset = 3; // still temporary


        // -------------------------------
        // BOX PROPERTIES
        // -------------------------------
        public override int? ActiveBoxBitOffset => _activeBoxBitOffset;
        public override int FirstBoxOffset => _boxOneOffset;
        public override int? ActiveBoxOffset => _activeBoxOffset;
        public override int BoxOccupancyOffset => _boxOccupancyOffset;
        public override int BoxHeaderSize => _boxHeaderSize;
        public override int BoxSize => _boxSize;
        public override int MaxBoxes => _maxBoxes;

        // -------------------------------
        // Pokemon PROPERTIES
        // -------------------------------

        public override int PokemonCount => _boxPokemonCountOffset;
        public override int PokemonSize => _pokemonSize;
        public override int NameOffset => _nameOffset;
        public override int LevelOffset => _levelOffset;

        // -------------------------------
        // METHODS
        // -------------------------------
        public override int PokedexIdFromByte(byte species)
        {
            return GenOneDictionary.ByteToPokedex[species];
        }
    }

    public class PokemonRed : PokemonBlue
    {
        // Inherit from Blue; override if needed later
    }
}