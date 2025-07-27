using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
        public abstract int CurrentHPOffset { get; }
        public abstract int HPXPOffset { get; }
        public abstract int LevelOffset { get; }

        // -------------------------------
        // UTILITIES
        // -------------------------------
        public abstract int PokedexIdFromByte(byte species);
        public abstract int CalculateMaxHP(int statXP, int baseHP, int dv, int level);
        public abstract int CalculateStat(int statXP, int baseHP, int dv, int level);
    }

    // -----------------------------------
    // POKEMON GENERATION ONE (RED, BLUE, YELLOW)
    // -----------------------------------
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
        private const int _nameOffset = 0x00;
        private const int _currentHPOffset = 0x01;
        private const int _HPXPOffset = 0x11;
        private const int _levelOffset = 0x03;


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
        public override int CurrentHPOffset => _currentHPOffset;
        public override int HPXPOffset => _HPXPOffset;
        
        public override int LevelOffset => _levelOffset;

        // -------------------------------
        // METHODS
        // -------------------------------
        public override int PokedexIdFromByte(byte species)
        {
            return GenOneDictionary.ByteToPokedex[species];
        }
        public override int CalculateMaxHP(int statXP, int baseHP, int dv, int level)
        {
            int statContribution = CalculateStatContribution(statXP, baseHP, dv, level);
            int maxHP = statContribution + 10 + level;
            return maxHP;
        }
        public override int CalculateStat(int statXP, int baseStat, int dv, int level)
        {
            int statContribution = CalculateStatContribution(statXP, baseStat, dv, level);
            int stat = statContribution + 5;
            return stat;
        }
        private int CalculateStatContribution(int statXP, int baseStat, int dv, int level)
        {
            int effectiveBase = (baseStat + dv) * 2;
            double sqrtStatXP = Math.Floor(Math.Sqrt(statXP) / 4);
            int scaledStat = (effectiveBase + (int)sqrtStatXP) * level;
            int finalValue = (int)Math.Floor(scaledStat / 100.0);
            return finalValue;
        }
    }

    public class PokemonRed : PokemonBlue
    {
        // Inherit from Blue; override if needed later
    }
}