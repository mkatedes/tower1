using System.Collections.Generic;
using tower1.Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using static tower1.Load.Enemies;
using System;

namespace tower1.Class_Manager
{
    internal class EnemyManager
    {
        public static List<Enemy> _enemies;
        private ContentManager _content;

        public EnemyManager(ContentManager content)
        {
            _content = content;
            _enemies = new List<Enemy>();
        }

        public void AddEnemy(string name, List<Texture2D> sprite, Vector2 position, int health, float speed, int reward)
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
            for (int i = 0; i < _enemies.Count; i++)
            {
                Enemy enemy = _enemies[i];
                if (enemy._health <= 0)
                {
                    _enemies[i]._state = 2;
                }
                // enemy._position += Vector2.Normalize(enemy._target - enemy._position) * enemy._speed
            }
            foreach(Enemy enemy in _enemies)
            {
                switch (enemy._state) { 
                    case 0:
                        enemy._sprite = _enemyStandAnimations[enemy._name];
                        break;
                    case 1:
                        enemy._sprite = _enemyWalkAnimations[enemy._name];
                        break;
                    case 2:
                        enemy._sprite = _enemyDeathAnimations[enemy._name];
                        break;
                    case 3:
                        enemy._sprite = _enemyAttackAnimations[enemy._name];
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, int framecount)
        {
            foreach (Enemy enemy in _enemies)
            {
                spriteBatch.Draw(enemy._sprite[framecount], enemy._position, Color.White);
            }
        }

        public List<Enemy> GetEnemies()
        {
            return _enemies;
        }

        public void LoadContent(ContentManager _content)
        {
            Load.Enemies.LoadEnemies(_content);
        }
        
    }
}
