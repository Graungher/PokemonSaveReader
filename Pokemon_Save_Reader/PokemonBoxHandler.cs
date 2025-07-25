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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public PokemonBoxHandler(byte[] fileBytes, GameVersion game)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
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
            if(boxNumber > _theGame.MaxBoxes)
                throw new InvalidOperationException("Invalid Box Selection");

            int boxOffsetFromFirst = _theGame.BoxSize * (boxNumber - 1);
            _currentBoxOffset = _theGame.FirstBoxOffset + boxOffsetFromFirst;

            if (_theGame.ActiveBoxOffset.HasValue && _theGame.ActiveBoxBitOffset.HasValue)
            {
                const byte ActiveBoxMask = 0x0F;
                int activeBox = (_saveFile[(int)_theGame.ActiveBoxBitOffset] & ActiveBoxMask) + 1;
                Console.WriteLine($"The Active box is: {activeBox}, and the selected box is {boxNumber}");  //debug line
                // Needed for Generation 1 where the active box is stored in a different memory location.
                if (boxNumber == activeBox)
                {
                    _currentBoxOffset = ((int)_theGame.ActiveBoxOffset);
                }
            }
            
        }

        // -------------------------------
        // PRIVATE METHODS - POKEMON HANDLING
        // -------------------------------

        private void GoToPokemon(int pokemon_index)
        {
            EnsureGameIsSet();
       
            int currentOccupantCountOffset = _theGame.BoxOccupancyOffset + (int)_currentBoxOffset;

            if(pokemon_index > _saveFile[currentOccupantCountOffset])
                throw new InvalidOperationException("Invalid Pokemon Selection");

            _currentPokemonOffset = _currentBoxOffset + _theGame.BoxHeaderSize;
            _currentPokemonOffset += (_theGame.PokemonSize * (pokemon_index - 1));
        }

        private void BuildPokemon() 
        {
            int speciesOffset = (int)_currentPokemonOffset + _theGame.NameOffset;
            byte speciesByte = _saveFile[speciesOffset];
            int pokedexId = _theGame.PokedexIdFromByte(speciesByte);
            _pokemon.Name = Pokedex.IdToName[pokedexId];

            int levelOffset = (int)_currentPokemonOffset + _theGame.LevelOffset;
            _pokemon.Level = _saveFile[levelOffset];
        }
    }
}
