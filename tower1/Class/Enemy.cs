using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace tower1.Class
{
    public class Enemy : Entity
    {
        public int _health { get; set;}
        public float _speed { get; set; }
        public int _reward { get; set; }
        public int _state { get; set; }
        // 0 = stand, 1 = walk, 2 = death 3 = attack

        public Enemy(string name, List<Texture2D> sprite, Vector2 position, int health, float speed, int reward)
            : base(name, sprite, position)
        {
            _health = health;
            _speed = speed;
            _reward = reward;
            _state = 0;
        }
    }
}
  