using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace tower1.Class
{
    internal class Bullet : Entity
    {
        public Enemy _target { get; set;}
        public float _speed { get; set;}
        public int _damage { get; set;}

        public Bullet(Texture2D texture, Vector2 position, Enemy target, float speed, int damage)
            : base("Bullet", texture, position)
        {
            _target = target;
            _speed = speed;
            _damage = damage;
        }
    }
}
