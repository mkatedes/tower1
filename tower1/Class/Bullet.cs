using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace tower1.Class
{
    public class Bullet : Entity
    {
        public Enemy _target { get; set;}
        public float _speed { get; set;}
        public int _damage { get; set;}

        public Bullet(List<Texture2D> texture, Vector2 position, Enemy target, float speed, int damage)
            : base("Bullet", texture, position)
        {
            _target = target;
            _speed = speed;
            _damage = damage;
        }
    }
}
