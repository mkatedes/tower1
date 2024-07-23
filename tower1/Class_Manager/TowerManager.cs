using tower1.Class;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace tower1.Class_Manager
{
    internal class TowerManager
    {
        public static List<Tower> _towers;

        public TowerManager()
        {
            _towers = new List<Tower>();
        }

        public void AddTower(string name, Texture2D sprite, Vector2 position, int damage, int range,
            double fireRate, int cost, string type, string description, Texture2D bulletSprite)
        {
            Tower tower = new Tower(name, sprite, position, damage, range,
                fireRate, cost, type, description, bulletSprite);
            _towers.Add(tower);
        }

        public void RemoveTower(Tower tower)
        {
            _towers.Remove(tower);
        }

        public void Update(GameTime gameTime, BulletManager bullets)
        {
            foreach (Tower tower in _towers)
            {
                foreach (Enemy enemy in EnemyManager._enemies)
                {
                    if(tower.CanShoot(enemy, gameTime))
                    {
                        bullets.AddBullet(tower._bulletSprite, tower._position, enemy, 5f, tower._damage);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in _towers)
            {
                spriteBatch.Draw(tower._sprite, tower._position, Color.White);
            }
        }

        public List<Tower> GetTowers()
        {
            return _towers;
        }
    }
}
