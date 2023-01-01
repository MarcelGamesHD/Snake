using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeProjekt
{
    // Das ist die Klasse für die Punkte, welche auf dem
    // Spielfeld aufgezeichnet werden. 
    internal class Kreis
    {
        public int x { get; set;  }
        public int y { get; set; }

        public Kreis()
        {
            x = 0;
            y = 0;
        }
    }
}
