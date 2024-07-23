
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
    }
}
