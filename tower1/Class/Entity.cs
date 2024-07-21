using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower1.Class
{
    internal class Entity
    {
        public string _name { get; set;}
        public Texture2D _sprite { get; set; }
        public Vector2 _position { get; set; }

        public Entity(string name, Texture2D sprite, Vector2 position)
        {
            _name = name;
            _sprite = sprite;
            _position = position;
        }
    }
}
