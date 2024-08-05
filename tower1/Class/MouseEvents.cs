using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace tower1.Class
{
    internal static class MouseEvents
    {
        public static void MouseEvent()
        {
            MouseState mouseState = Mouse.GetState();
            //permets de déplacer le sprite avec la souris au point cliqué
            if (mouseState.RightButton == ButtonState.Pressed && _focus != null)
            {
                GetDestination(mouseState);
            }

            Vector2 mousePosition = mouseState.Position.ToVector2(); // Transformation en Vector2

            // Exemple d'utilisation dans une comparaison


            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (Vector2.Distance(mousePosition, _gameInterface._buildButtonPosition) < 50)
                {
                    _gameInterface.Build();
                }
                bool _hasFocus = false;
                foreach (Enemy enemy in _enemies.GetEnemies())
                {
                    if (enemy._position.X < mouseState.Position.X &&
                        enemy._position.X + enemy._sprite.Width > mouseState.Position.X &&
                        enemy._position.Y < mouseState.Position.Y &&
                        enemy._position.Y + enemy._sprite.Height > mouseState.Position.Y)
                    {
                        _focus = enemy;
                        _hasFocus = true;
                    }
                }
                if (!_hasFocus)
                {
                    _focus = null;
                }
            }
        }

        protected static void GetDestination(MouseState mouseState, Vector2 positionFinal)
        {
            positionFinal.X = mouseState.Position.X;
            positionFinal.Y = mouseState.Position.Y;

            // Check si le sprite est dans la fenêtre
            if (positionFinal.X < 0)
                positionFinal.X = 0;
            if (positionFinal.Y < 0)
                positionFinal.Y = 0;
            else
                _positionFinal.X = GraphicsDevice.Viewport.Width;
        }
    }
}
