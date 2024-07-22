using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace tower1.Class
{
    internal class Tower : Entity
    {
        public int _damage;
        public int _range;
        public double _fireRate;
        public int _cost;
        public string _type;
        public string _description;
        public Texture2D _bulletSprite; 
        public double LastShootTime;

        public Tower(string name, Texture2D sprite, Vector2 position, int damage, int range, 
            double fireRate, int cost, string type, string description, Texture2D bulletSprite)
            : base(name, sprite, position)
        {
            _damage = damage;
            _range = range;
            _fireRate = fireRate;
            _cost = cost;
            _type = type;
            _name = name;
            _description = description;
            _sprite = sprite;
            _bulletSprite = bulletSprite;
            LastShootTime = 0;
        }

        public void Shoot(Enemy enemy)
        {
            enemy._health -= this._damage;
        }

        public bool CanShoot(Enemy enemy, GameTime gameTime)
        {
            // Convertissez le temps de recharge en secondes
            double cooldown = _fireRate;
            double timeSinceLastShoot = gameTime.TotalGameTime.TotalSeconds - LastShootTime;
            // Si le temps écoulé depuis le dernier tir est supérieur ou égal au temps de recharge
            // et que la distance entre la tour et l'ennemi est inférieure à la portée de la tour
            if (timeSinceLastShoot >= cooldown &&
                Vector2.Distance(this._position, enemy._position) < this._range)
            {
                this.Shoot(enemy);
                LastShootTime = gameTime.TotalGameTime.TotalSeconds;
                return true;
            }
            return false;
        }

    }
}
