using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Save_Reader
{
    
    class Pokemon_Box_Handler
    {
        private readonly byte[] _save_file;
        private GameVersion _game;
        private IPokemonGame _theGame;
        private long _current_file_location;

        private long _box_offset = 0;
        private long _current_box;
        private long _current_byte;

        private long _pokemon_offset = 0;
        private long _current_pokemon;

        // gives the handler the file in byte array form
        public Pokemon_Box_Handler(byte[] fileBytes) => _save_file = fileBytes;

        public enum GameVersion
        {
            Blue,
            Red,
            Gold
        }
        
        // sets the game version
        public void SetGame(GameVersion version)
        {
            _game =(GameVersion)version;
            bool no_game = false;

            switch (_game)
            {
               case GameVersion.Red:
                    _theGame = new Pokemon_Red();
                    break;

               case GameVersion.Blue:
                    _theGame = new Pokemon_Blue();
                    break;

                default:
                    no_game = true;
                    break;
            }
            if (!no_game)
            {
                _box_offset = (long)_theGame.get_box_1_offset();
                _pokemon_offset = (long)_theGame.get_pokemon_size();
            }
        }

        public void GoToBox(int box_number)
        {
            EnsureGameIsSet();
            _current_file_location = _box_offset + (_theGame.get_box_size() * (box_number - 1));
            _current_box = _current_file_location;
        }

        public void GoToPokemon(int pokemon_index)
        {
            EnsureGameIsSet();
            _current_file_location = _current_box + _theGame.get_box_header_offset();
            _current_file_location +=(_pokemon_offset * (pokemon_index - 1));
            _current_pokemon = _current_file_location;
            
            ShowByte();
        }

        private void ShowByte()
        {
            EnsureGameIsSet();
            byte temp = _save_file[(int)_current_file_location];
            //String monName = Pokedex.Entry[(int)_current_file_location];

            Console.WriteLine($"byte at 0x{_current_file_location:X4} is: 0x{_save_file[(int)_current_file_location]:X2} and that is {Pokedex.Entry[_theGame.getSpecies(_save_file[(int)_current_file_location])]}");
        }
        private void EnsureGameIsSet()
        {
            if (_theGame == null)
                throw new InvalidOperationException("Game version not set. Call SetGame() first.");
        }
    }
}
