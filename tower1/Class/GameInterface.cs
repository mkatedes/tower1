
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace tower1.Class
{
    internal class GameInterface
    {
        public bool _showGrid;
        public bool _showTowerRange;
        public bool _showTowerInfo;
        public bool _showEnemyInfo;
        public Texture2D _buildButton;
        public Vector2 _buildButtonPosition;

        public GameInterface(Texture2D buildButton, Vector2 buildButtonPosition)
        {
            _buildButtonPosition = buildButtonPosition;
            _buildButton = buildButton;
            _showGrid = false;
            _showTowerRange = false;
            _showTowerInfo = false;
            _showEnemyInfo = false;
        }

        public void Build()
        {
            _showGrid = !_showGrid;
            _showTowerRange = !_showTowerRange;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_buildButton, _buildButtonPosition, Color.White);
        }

        protected void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, Texture2D pixel)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            spriteBatch.Draw(pixel,
                new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 1),
                null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
        }

        public void DrawL(SpriteBatch spriteBatch, GameTime gameTime, Grid grid, Texture2D pixel)
        {

            for (int col = 0; col <= grid._width / grid._cellSize; col++)
            {
                var x = col * grid._cellSize;
                DrawLine(spriteBatch, new Vector2(x, 0), new Vector2(x, grid._height), Color.Black, pixel);
            }
            for (int row = 0; row <= grid._height / grid._cellSize; row++)
            {
                var y = row * grid._cellSize;
                DrawLine(spriteBatch, new Vector2(0, y), new Vector2(grid._width, y), Color.Black, pixel);
            }
        }
    }
}
