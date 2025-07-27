using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PokemonSaveReader
{
    
    public class PokemonBoxHandler
    {
        private readonly byte[] _saveFile;
        private PokemonGame _theGame;
        private PokemonClass _pokemon;

        private long _currentBoxOffset;
        private long _currentPokemonOffset;

        // Constructor
        // gives the handler the file in byte array form
#pragma warning disable CS8618 
        public PokemonBoxHandler(byte[] fileBytes, GameVersion game)
#pragma warning restore CS8618 
        {
            _saveFile = fileBytes;
            SetGame(game);
        }

        // -------------------------------
        // PUBLIC METHODS
        // -------------------------------

        public PokemonClass GetPokemonFromBox(int boxNumber, int slotNumber)
        {
            GoToBox(boxNumber);
            GoToPokemon(slotNumber);
            BuildPokemon();
            return _pokemon;
        }
        public String GetPokemonNameFromBox(int boxNumber, int slotNumber)
        {
            EnsureGameIsSet();
            GoToBox(boxNumber);
            GoToPokemon(slotNumber);

            int speciesOffset = (int)_currentPokemonOffset + _theGame.NameOffset;
            byte speciesByte = _saveFile[speciesOffset];
            int pokedexId = _theGame.PokedexIdFromByte(speciesByte);
            String pokemonName = Pokedex.IdToName[pokedexId];

            return pokemonName;
        }
        public int BoxOccupantCount(int boxNumber)
        {
            EnsureGameIsSet();
            long tempBoxOffset = _currentBoxOffset;

            GoToBox(boxNumber);                 // need to temerarally destroy current box to send info about a different box
            long boxOccupantOffset = _theGame.BoxOccupancyOffset + _currentBoxOffset;

            _currentBoxOffset = tempBoxOffset;  // restore original box Stuff

            int pokemonCount = _saveFile[(int)boxOccupantOffset];
            if (pokemonCount > 20)
                pokemonCount = 0;               // if invalid pokemon count, return 0

            //Console.WriteLine($"0x{boxOccupantOffset:X}");
            return pokemonCount;
        }
        public int MaxBoxes()
        {
            EnsureGameIsSet();
            return _theGame.MaxBoxes;
        }

        // -------------------------------
        // PRIVATE METHODS - GAME SETUP
        // -------------------------------

        private void SetGame(GameVersion game)
        {
            switch (game)
            {
               case GameVersion.Red:
                    _theGame = new PokemonRed();
                    _pokemon = new PokemonGenOne(_saveFile);
                    break;

               case GameVersion.Blue:
                    _theGame = new PokemonBlue();
                    _pokemon = new PokemonGenOne(_saveFile);
                    break;

                default:
                    throw new ArgumentException($"Unsupported game version: {game}");
            }
            GoToBox(1);
            GoToPokemon(1);
        }
        private void EnsureGameIsSet()
        {
            if (_theGame == null)
                throw new InvalidOperationException("Game version not set.");
        }

        // -------------------------------
        // PRIVATE METHODS - BOX HANDLING
        // -------------------------------

        private void GoToBox(int boxNumber)
        {
            
            EnsureGameIsSet();
            if(boxNumber > _theGame.MaxBoxes || boxNumber <= 0)
                throw new InvalidOperationException("Invalid Box Selection");

            int boxOffsetFromFirst = _theGame.BoxSize * (boxNumber - 1);
            _currentBoxOffset = _theGame.FirstBoxOffset + boxOffsetFromFirst;

            if (_theGame.ActiveBoxOffset.HasValue && _theGame.ActiveBoxBitOffset.HasValue)
            {
                const byte ActiveBoxMask = 0x0F;
                int activeBox = (_saveFile[(int)_theGame.ActiveBoxBitOffset] & ActiveBoxMask) + 1;
                // Console.WriteLine($"The Active box is: {activeBox}, and the selected box is {boxNumber}");  //debug line

                // Needed for Generation 1 where the active box is stored in a different memory location.
                if (boxNumber == activeBox)
                {
                    _currentBoxOffset = ((int)_theGame.ActiveBoxOffset);
                }
            }
            //Console.WriteLine($"This Is Box {boxNumber}");
        }

        // -------------------------------
        // PRIVATE METHODS - POKEMON HANDLING
        // -------------------------------

        private void GoToPokemon(int pokemon_index)
        {
            EnsureGameIsSet();
       
            int currentOccupantCountOffset = _theGame.BoxOccupancyOffset + (int)_currentBoxOffset;

            if(pokemon_index > _saveFile[currentOccupantCountOffset] || pokemon_index <= 0)
                throw new InvalidOperationException("Invalid Pokemon Selection");

            _currentPokemonOffset = _currentBoxOffset + _theGame.BoxHeaderSize;
            _currentPokemonOffset += (_theGame.PokemonSize * (pokemon_index - 1));

            //Console.WriteLine($"This Is Pokemon Index {pokemon_index}");
        }

        private void BuildPokemon() 
        {
            //Name
            int speciesOffset = (int)_currentPokemonOffset + _theGame.NameOffset;
            byte speciesByte = _saveFile[speciesOffset];
            int pokedexId = _theGame.PokedexIdFromByte(speciesByte);
            _pokemon.Name = Pokedex.IdToName[pokedexId];

            //Current HP
            int currentHPOffset = (int)_currentPokemonOffset + _theGame.CurrentHPOffset;
            _pokemon.CurrentHP = (int)ReadBigEndian(_saveFile, currentHPOffset, 2);
//            Console.WriteLine($"HP Value is: {_pokemon.CurrentHP} Offset is: 0X{currentHPOffset:X}");

            //Level
            int levelOffset = (int)_currentPokemonOffset + _theGame.LevelOffset;
            _pokemon.Level = _saveFile[levelOffset];

            //Max HP
            int maxHPOffset = (int)_currentPokemonOffset + _theGame.HPXPOffset;
            int hpXP = (int)ReadBigEndian(_saveFile, maxHPOffset, 2);
            _pokemon.MaxHP = _theGame.CalculateMaxHP(hpXP, 30, 2, _pokemon.Level) ?? 0;                                    /////// HARD CODED BUT WORKS!!!
                                                                                                           //            Console.WriteLine($"HP Value is: {_pokemon.MaxHP} Offset is: 0X{maxHPOffset:X}");
        }

        // reverses 1-8 BYTES    ex: 0x1234 --> 0x3412
        ulong ReadBigEndian(byte[] data, int offset, int byteCount)
        {
            if (byteCount < 1 || byteCount > 8)
                throw new ArgumentOutOfRangeException(nameof(byteCount), "Must be between 1 and 8 bytes.");

            ulong value = 0;
            for (int i = 0; i < byteCount; i++)
            {
                value <<= 8;                // shifts ALL the BITS over 8 (moves the BYTES left)
                value |= data[offset + i];  // ORs the last byte of the new array with the next byte (ORs 0 with __)
            }
            return value;
        }
    }
}
