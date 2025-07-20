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
        private PokemonGame _theGame;
        private PokemonClass _pokemon;
        private long _currentFileOffset;

        private long _currentBox;
        private int _activeBox;

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
                    break;
            }
        }

        private void GoToBox(int box_number)
        {
            
            EnsureGameIsSet();
            long _currentFileOffset = _theGame.FirstBoxOffset + _theGame.BoxSize * (box_number - 1);
            if (_theGame.ActiveBoxOffset.HasValue && _theGame.ActiveBoxBitOffset.HasValue)
            {
                _activeBox = (_saveFile[(int)_theGame.ActiveBoxBitOffset] & 0x0F) + 1;
                if(box_number == _activeBox)
                {
                    _currentFileOffset = ((int)_theGame.ActiveBoxOffset);
                }
            }
            _currentBox = _currentFileOffset;
            Console.WriteLine($"The Active box is: {_activeBox}, and the selected box is {box_number}");
        }

        private void GoToPokemon(int pokemon_index)
        {
            EnsureGameIsSet();
            _currentFileOffset = _currentBox + _theGame.BoxHeaderOffset;
            _currentFileOffset += (_theGame.PokemonSize * (pokemon_index - 1));
            _currentPokemonOffset = _currentFileOffset;
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
