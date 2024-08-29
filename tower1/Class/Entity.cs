using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace tower1.Class
{
    public class Entity
    {
        public string _name { get; set;}
        public List<Texture2D> _sprite { get; set; }
        public Vector2 _position { get; set; }

        public Entity(string name, List<Texture2D> sprite, Vector2 position)
        {
            _name = name;
            _sprite = sprite;
            _position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite[0], _position, Color.White);
        }
    }
}
