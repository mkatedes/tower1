using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower1.Class
{
    internal class Enemy : Entity
    {
        public int _health { get; set;}
        public float _speed { get; set; }
        public int _reward { get; set; }

        public Enemy(string name, Texture2D sprite, Vector2 position, int health, float speed, int reward)
            : base(name, sprite, position)
        {
            _health = health;
            _speed = speed;
            _reward = reward;
        }
    }
}
