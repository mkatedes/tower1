using System.Collections.Generic;
using tower1.Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower1.Class_Manager
{
    internal class EnemyManager
    {
        public static List<Enemy> _enemies;

        public EnemyManager()
        {
            _enemies = new List<Enemy>();
        }

        public void AddEnemy(string name, Texture2D sprite, Vector2 position, int health, float speed, int reward)
        {
            Enemy enemy = new Enemy(name, sprite, position, health, speed, reward);
            _enemies.Add(enemy);
        }
        public void RemoveEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
        }

        public void Update()
        {
            foreach (Enemy enemy in _enemies)
            {
               // enemy._position += Vector2.Normalize(enemy._target - enemy._position) * enemy._speed
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in _enemies)
            {
                spriteBatch.Draw(enemy._sprite, enemy._position, Color.White);
            }
        }

        public List<Enemy> GetEnemies()
        {
            return _enemies;
        }

        
    }
}
