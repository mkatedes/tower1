using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace tower1.Class
{
    internal class Tower : Entity
    {
        public int _damage;
        public int _range;
        public int _fireRate;
        public int _cost;
        public string _type;
        public string _description;
        public Texture2D _bulletSprite;

        public Tower(string name, Texture2D sprite, Vector2 position, int damage, int range, 
            int fireRate, int cost, string type, string description, Texture2D bulletSprite)
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
        }

        public async Task Shoot()
        {
            await Task.Delay(this._fireRate);   

        }
    }
}
