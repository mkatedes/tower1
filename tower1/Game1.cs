using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using tower1.Class;
using tower1.Class_Manager;
using tower1.Load;

namespace tower1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private EnemyManager _enemies;

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

            _enemies = new EnemyManager(Content);


             // Récupère la résolution actuelle de l'écran
            int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            // Définit la résolution du jeu à celle de l'écran
            _graphics.PreferredBackBufferWidth = screenWidth - 300;
            _graphics.PreferredBackBufferHeight = screenHeight - 300;

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
            
            _enemies.LoadContent(Content);

            _enemies.AddEnemy("scorpion", Load.Enemies._enemyStandAnimations["scorpion"],
                new Vector2(10, 10), 100, 2, 10);

            List<Texture2D> Textures = new List<Texture2D>();
            Textures.Add(Content.Load<Texture2D>("fire_tower"));
            Textures.Add(Content.Load<Texture2D>("fire_tower"));

            List<Texture2D> Textures1 = new List<Texture2D>();
            Textures1.Add(Content.Load<Texture2D>("fire_bullet"));
            Textures1.Add(Content.Load<Texture2D>("fire_bullet"));

            _towers.AddTower("Fire Tower", Textures,
                new Vector2(400, 100), 10, 100, 1, 1000, "fire", "This is a fire tower",
                Textures1
                );

            Textures[0] = Content.Load<Texture2D>("ice_tower");
            Textures[1] = Content.Load<Texture2D>("ice_tower");
            Textures1[0] = Content.Load<Texture2D>("ice_bullet");
            Textures1[1] = Content.Load<Texture2D>("ice_bullet");

            _towers.AddTower("Ice Tower", Textures,
                new Vector2(200, 100), 10, 100, 1, 1000, "ice", "This is a ice tower",
                Textures1);

            _gameInterface = new GameInterface(Content.Load<Texture2D>("button_up"), new Vector2(500, 620));
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseEvents.MouseEvent(_enemies, _gameInterface);
            _towers.Update(gameTime, _bullets);
            _enemies.Update();
            _bullets.Update();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            int frameCount = CalcCurrentFrame(gameTime, 19, 10);
            GraphicsDevice.Clear(Color.SandyBrown);

            _spriteBatch.Begin();

            _enemies.Draw(_spriteBatch, frameCount);
            _towers.Draw(_spriteBatch);
            _bullets.Draw(_spriteBatch);
            _gameInterface.Draw(_spriteBatch);
            if (_gameInterface._showGrid)
                _gameInterface.DrawL(_spriteBatch, gameTime, _grid, _pixel);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        protected int CalcCurrentFrame(GameTime gameTime, int frameCount, int frameSpeed)
        {
            return (int)(gameTime.TotalGameTime.TotalSeconds * frameSpeed) % frameCount;
        }

    }
}
