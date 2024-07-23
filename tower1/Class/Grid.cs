
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Windows.Forms;
using System;

namespace tower1.Class
{
    internal class Grid
    {
        public int _width;
        public int _height;
        public int _cellSize;
        public Cell[,] _grid;

        public Grid(int width, int height, int cellSize)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _grid = new Cell[_width, _height];
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _grid[x, y] = new Cell(x, y);
                }
            }
        }

        

        
    }
}

