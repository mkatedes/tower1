using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XAudio2;
using System;
using System.Diagnostics;
using tower1.Class;
using tower1.Class_Manager;

namespace tower1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Enemy _focus = null;
        private Vector2 _positionFinal = new Vector2(0, 0);
        private EnemyManager _enemies = new EnemyManager();
        private TowerManager _towers = new TowerManager();
        private BulletManager _bullets = new BulletManager();
        private Grid _grid;
        private GameInterface _gameInterface;
        private Texture2D _pixel;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Récupère la résolution actuelle de l'écran
            int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            // Définit la résolution du jeu à celle de l'écran
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight - 60;

            _grid = new Grid(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, 50);
            //_graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _enemies.AddEnemy("Spider", Content.Load<Texture2D>("down_spider"), 
                new Vector2(10, 10), 100, 2, 10);

            _towers.AddTower("Fire Tower", Content.Load<Texture2D>("fire_tower"), 
                new Vector2(400, 100), 10, 100, 1, 1000, "fire", "This is a fire tower", 
                Content.Load<Texture2D>("fire_bullet"));

            _towers.AddTower("Ice Tower", Content.Load<Texture2D>("ice_tower"),
                new Vector2(200, 100), 10, 100, 1, 1000, "ice", "This is a ice tower",
                Content.Load<Texture2D>("ice_bullet"));

            _gameInterface = new GameInterface(Content.Load<Texture2D>("button_up"), new Vector2(500, 620));
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseEvents();
            _towers.Update(gameTime, _bullets);
            _enemies.Update();
            _bullets.Update();
            MoveSprite();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SandyBrown);

            _spriteBatch.Begin();

            _enemies.Draw(_spriteBatch);
            _towers.Draw(_spriteBatch);
            _bullets.Draw(_spriteBatch);
            _gameInterface.Draw(_spriteBatch);
            if (_gameInterface._showGrid)
                DrawL(_spriteBatch);

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

            if (Vector2.Distance(_focus._position, _positionFinal) < 1f)
            {
                CancelDestination();
                return;
            }
            _focus._position += direction * _focus._speed; // Déplacement du sprite dans la direction calculée
        }

        public void MouseEvents()
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
        protected void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            spriteBatch.Draw(_pixel,
                new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 1),
                null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
        }

        protected void DrawL(SpriteBatch spriteBatch)
        {
            for (int col = 0; col <= _grid._width / _grid._cellSize; col++)
            {
                var x = col * _grid._cellSize;
                DrawLine(spriteBatch, new Vector2(x, 0), new Vector2(x, _grid._height), Color.Black);
            }
            for (int row = 0; row <= _grid._height / _grid._cellSize; row++)
            {
                var y = row * _grid._cellSize;
                DrawLine(spriteBatch, new Vector2(0, y), new Vector2(_grid._width, y), Color.Black);
            }
        }
    }
}
