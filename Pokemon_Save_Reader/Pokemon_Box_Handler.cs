using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Save_Reader
{
    
    class Pokemon_Box_Handler
    {
        private byte[] _save_file;
        private GameVersion _game;
        private IPokemonGame theGame;
        private long current_byte;

        // gives the handler the file in byte array form
        public Pokemon_Box_Handler(byte[] fileBytes) => _save_file = fileBytes;

        private enum GameVersion
        {
            Blue,
            Red,
            Gold
        }
        
        // sets the game version
        public void set_Game(int version)
        {
            _game =(GameVersion)version;

            switch (_game)
            {
               case GameVersion.Red:
                    theGame = new Pokemon_Red();
                    break;

               case GameVersion.Blue:
                    theGame = new Pokemon_Blue();
                    break;

                default:
                    break;
            }
        }

        public void go_to_box(int box_number)
        {
            long box_offset = (long)theGame.get_box_1_offset();
            current_byte = (long)box_offset + (theGame.get_box_size() * ((long)box_number - 1));
            current_byte += theGame.get_box_header_offset();

            showByte();
        }

        private void showByte()
        {
            Console.WriteLine($"byte at 0x{current_byte:X4} is: 0x{_save_file[current_byte]:X2} ");
        }

    }
}
