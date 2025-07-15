using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Save_Reader
{
    public abstract class Pokemon_Class
    {
        protected String _Name = string.Empty;
        protected int _Level = 1;

        public Pokemon_Class() { }
        public void setName(String inputName) => _Name = inputName;
        public String getName() => _Name;
        public void setLevel(int inputLevel) => _Level = inputLevel;
        public int getLevel() => _Level;
    }

    public class Gen1Pokemon : Pokemon_Class
    {

        // temp everything
        private int x;
        public void SetTemp(int i) => x = i;
        public int getTemp() => x;
    }

}
