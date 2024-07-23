using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tower1.Class
{
    internal class Cell
    {
        public bool _isOccupied;
        //Change a obstacle to a walkable cell
        public bool _isWalkable;
        public Tower _tower;
        public int _x;
        public int _y;

        public Cell(int x, int y)
        {
            _x = x;
            _y = y;
            _isOccupied = false;
            _isWalkable = true;
            _tower = null;
        }

    }
}
