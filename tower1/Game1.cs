using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using tower1.Class;

namespace tower1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<Tower> _towers = new List<Tower>();
        List<Enemy> _enemies = new List<Enemy>();
        List<Texture2D> _bullets = new List<Texture2D>();
        private Enemy _focus = null;
        private Vector2 _positionFinal = new Vector2(0, 0);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Enemy enemy1 = new Enemy("Spider", Content.Load<Texture2D>("down_spider"), 
                new Vector2(10, 10), 100, 1, 10);
            Tower fireTower = new Tower("Fire Tower", Content.Load<Texture2D>("fire_tower"), 
                new Vector2(400, 100), 10, 10, 1, 10, "fire", "This is a fire tower", 
                Content.Load<Texture2D>("fire_bullet"));
            Tower iceTower = new Tower("Ice Tower", Content.Load<Texture2D>("ice_tower"),
                new Vector2(200, 100), 10, 10, 1, 10, "ice", "This is a ice tower",
                Content.Load<Texture2D>("ice_bullet"));

            _towers.Add(fireTower);
            _towers.Add(iceTower);
            _enemies.Add(enemy1);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouseState = Mouse.GetState();
            //permets de déplacer le sprite avec la souris au point cliqué
            if (mouseState.RightButton == ButtonState.Pressed && _focus != null)
            {
                GetDestination(mouseState);
                CheckRangeTower();
            }
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                bool _hasFocus = false;
                foreach (Enemy enemy in _enemies)
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
            MoveSprite();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SandyBrown);

            _spriteBatch.Begin();

            foreach (Tower tower in _towers)
            {
                _spriteBatch.Draw(tower._sprite, tower._position, Color.White);
            }
            foreach (Enemy enemy in _enemies)
            {
                _spriteBatch.Draw(enemy._sprite, enemy._position, Color.White);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void GetDestination(MouseState mouseState)
        {
            _positionFinal.X = mouseState.Position.X;
            _positionFinal.Y = mouseState.Position.Y;

            // Check si le sprite est dans la fenêtre
            if (_positionFinal.X < 0)
                _positionFinal.X = 0;
            else if (_positionFinal.X > GraphicsDevice.Viewport.Width)
                _positionFinal.X = GraphicsDevice.Viewport.Width;

            if (_positionFinal.Y < 0)
                _positionFinal.Y = 0;
            else if (_positionFinal.Y > GraphicsDevice.Viewport.Height)
                _positionFinal.Y = GraphicsDevice.Viewport.Height;
        }

        // Annule la destination du sprite
        // todo
        protected void CancelDestination()
        {
            _positionFinal = _focus._position;
        }

        protected void MoveSprite()
        {
            if (_focus == null)
                return;
            Vector2 direction = _positionFinal - _focus._position; // Calcul de la direction du déplacement
            direction.Normalize(); // Normalisation de la direction pour obtenir un vecteur unitaire

            _focus._position += direction * _focus._speed; // Déplacement du sprite dans la direction calculée
        }

        protected async void CheckRangeTower()
        {
            if (_focus == null)
                return;
            foreach (Tower tower in _towers)
            {
                if (tower._range >= Vector2.Distance(tower._position, _focus._position))
                {
                    tower.Shoot();
                    _bullets.Add(tower._bulletSprite);
                }
            }
        }

        protected void DrawBullet()
        {
            foreach (Texture2D bullet in _bullets)
            {
                if (bullet.position.X != 0 && bullet.position.Y != 0)
                {
                    _spriteBatch.Draw(bullet, _focus._position, Color.White);

                }
                
            }
        }
    }
}
