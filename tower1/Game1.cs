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

            MouseEvents.MouseEvent();
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
                _gameInterface.DrawL(_spriteBatch, gameTime, _grid, _pixel);

            _spriteBatch.End();
            base.Draw(gameTime);
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
        
    }
}
