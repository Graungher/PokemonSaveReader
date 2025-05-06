using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Save_Reader
{

    internal class Pokemon_Box_Handler
    {
        private enum GameVersion
        {
            Blue,
            Red,
            Gold
        }
        private GameVersion _game;


        public void doStuff()
        {
            _game = GameVersion.Red;
            IPokemonGame theGame;


            if (_game == GameVersion.Red) 
            { 
                theGame = new Pokemon_Red();
            }
            else 
            { 
                theGame = new Pokemon_Blue();
            }

            for (int i = 1; i < 152; i++) 
            {
                Console.WriteLine($"{i,3}:  {Pokedex.Entry[i]}");
            }

            int guy = theGame.getSpecies(0x01);

           //Console.WriteLine(Pokedex.Entry[guy]);

        }

    }

}
