using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tower1.Class_Manager;

namespace tower1.Class
{
    internal static class MouseEvents
    {
        private static Enemy _focus = null;
        private static Vector2 _positionFinal = new Vector2(0, 0);

        public static void MouseEvent(EnemyManager _enemies, GameInterface _gameInterface)
        {
            
            MouseState mouseState = Mouse.GetState();
            //permets de déplacer le sprite avec la souris au point cliqué
            if (mouseState.RightButton == ButtonState.Pressed && _focus != null)
            {
                GetDestination(mouseState);
                MoveSprite(_focus);
                _focus._state = 1;
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
                        enemy._position.X + enemy._sprite[0].Width > mouseState.Position.X &&
                        enemy._position.Y < mouseState.Position.Y &&
                        enemy._position.Y + enemy._sprite[0].Height > mouseState.Position.Y)
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

        static void GetDestination(MouseState mouseState)
        {
            _positionFinal.X = mouseState.Position.X;
            _positionFinal.Y = mouseState.Position.Y;

            // Check si le sprite est dans la fenêtre
            if (_positionFinal.X < 0)
                _positionFinal.X = 0;
            if (_positionFinal.Y < 0)
                _positionFinal.Y = 0;
           // if (_positionFinal.Y > 1000)
             //   _positionFinal.X = GraphicsDevice.Viewport.Width;
        }

        static void MoveSprite(Enemy enemy)
        {
            System.Diagnostics.Debug.WriteLine(_positionFinal);

            if (_focus == null)
                return;
            Vector2 direction = _positionFinal - enemy._position; // Calcul de la direction du déplacement
            direction.Normalize(); // Normalisation de la direction pour obtenir un vecteur unitaire

            if (Vector2.Distance(_focus._position, _positionFinal) < 1f)
            {
                _positionFinal = _focus._position;
                _focus._state = 0;
                return;
            }
            _focus._position += direction * _focus._speed; // Déplacement du sprite dans la direction calculée
        }
    }
}
