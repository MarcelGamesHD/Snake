using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeProjekt
{
    // Diese Klasse ist zum Einstellen von Width und Height.
    internal class Einstellungen
    {
        public static int Width { get; set; }
        public static int Height { get; set; }

        public static string directions;

        public Einstellungen()
        {
            Width = 16;
            Height = 16;
            directions = "left";
        }
    }
}
