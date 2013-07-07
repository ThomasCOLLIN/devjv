using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ROTP.Interface
{
    public class ResizeBar
    {
        private Vector2 _position;
        private Texture2D _texture;
        private String _textureName;
        private Int32 _maxLife;
        private Int32 _life;
        private Int32 _width;

        public ResizeBar(Vector2 position, String textureName, Int32 width, Int32 maxLife, Int32 life)
        {
            _position = position;
            _textureName = textureName;
            _maxLife = maxLife;
            _life = life;
            _width = width;
        }

        public void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>(_textureName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Double width = ((Double)_life / (Double)_maxLife) * (Double)_width;
            Rectangle lifeRect = new Rectangle((Int32)_position.X, (Int32)_position.Y, (Int32)width, _texture.Height);

            spriteBatch.Begin();

            spriteBatch.Draw(_texture, lifeRect, Color.White);

            spriteBatch.End();
        }

        public void ChangeLife(Int32 lifePoints)
        {
            _life = lifePoints * 10;
            if (_life > _maxLife)
            {
                _life = _maxLife;
                return;
            }
            if (_life < 0)
            {
                _life = 0;
                return;
            }
        }
    }
}
