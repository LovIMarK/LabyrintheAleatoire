using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinthe_Complex
{

   
    class Player
    {

        public int PlaX { get; set; }
        public int PlaY { get; set; }
        public Player(int plaX, int plaY)
        {
            this.PlaX = plaX;
            this.PlaY = plaY;
        }
    }
}
