using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using tower1.Class;

namespace tower1.Class_Manager
{
    internal class BulletManager
    {
        private static List<Bullet> _bullets;

        public BulletManager()
        {
            _bullets = new List<Bullet>();
        }

        public void AddBullet(List<Texture2D> texture, Vector2 position, Enemy target, float speed, int damage)
        {
            Bullet bullet = new Bullet(texture, position, target, speed, damage);
            _bullets.Add(bullet);
        }

        public void Update()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = _bullets[i];

                bullet._position += Vector2.Normalize(bullet._target._position - bullet._position)
                    * bullet._speed;

                // Check if the bullet has reached the target
                if (Vector2.Distance(bullet._position, bullet._target._position) < 3f)
                {
                    // Destroy the bullet instance
                    bullet._target._health -= bullet._damage;
                    _bullets.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in _bullets)
            {
                spriteBatch.Draw(bullet._sprite[0], bullet._position, Color.White);
            }
        }
    }
}
