using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PokemonSaveReader
{
    
    class PokemonBoxHandler
    {
        private readonly byte[] _saveFile;
        private readonly GameVersion _gameVersion;
        private IPokemonGame _theGame;
        private PokemonClass _pokemon;
        private long _currentFileOffset;

        private long _boxOffset = 0;
        private long _currentBox;
        private byte _current_byte;

        private long _pokemonOffset = 0;
        private long _currentPokemonOffset;

        // gives the handler the file in byte array form
        public PokemonBoxHandler(byte[] fileBytes, GameVersion game)
        {
            _gameVersion = game;
            _saveFile = fileBytes;
            SetGame();
        }

        // sets the game version
        public void SetGame()
        {
            bool no_game = false;

            switch (_gameVersion)
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
                    no_game = true;
                    break;
            }
            if (!no_game)
            {
                _boxOffset = (long)_theGame.FirstBoxOffset;
                _pokemonOffset = (long)_theGame.PokemonSize;
            }
        }

        public void GoToBox(int box_number)
        {
            EnsureGameIsSet();
            _currentFileOffset = _boxOffset + (_theGame.BoxSize * (box_number - 1));
            _currentBox = _currentFileOffset;
        }

        public void GoToPokemon(int pokemon_index)
        {
            EnsureGameIsSet();
            _currentFileOffset = _currentBox + _theGame.BoxHeaderOffset;
            _currentFileOffset +=(_pokemonOffset * (pokemon_index - 1));
            _currentPokemonOffset = _currentFileOffset;
            
            BuildPokemon();
        }

        public void BuildPokemon() 
        {
            byte _current_byte = _saveFile[(int)_currentPokemonOffset + _theGame.NameOffset];
            _pokemon.Name = Pokedex.IdToName[_theGame.PokedexIdFromByte(_current_byte)];

            _pokemon.Level = _saveFile[(int)_currentPokemonOffset + _theGame.LevelOffset];

            if (_pokemon is PokemonGenOne gen1)
            {
                gen1.PrintDetails();
            }
        }

        private void EnsureGameIsSet()
        {
            if (_theGame == null)
                throw new InvalidOperationException("Game version not set.");
        }
    }
}
