using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Save_Reader
{
    public interface IPokemonGame
    {
        int getSpecies(byte species);
        int get_box_1_offset();
        int get_box_header_offset();
        int get_box_size();
        int get_pokemon_size();
        int get_name_offset();
        int get_level_offset();
    }
}
