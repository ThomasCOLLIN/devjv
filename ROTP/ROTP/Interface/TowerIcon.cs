using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROTP.Interface
{
    public class TowerIcon
    {
        private Vector2 _position;
        private Texture2D _texture;
        private Rectangle _towerIconBoundsRect;
        private Int32 _sizeIcon;
        private String _textureName;
        public String Text { get; set; }

        public TowerIcon(Vector2 position, String textureName)
        {
            _position = position;
            _textureName = textureName;
            _sizeIcon = 45;
            _towerIconBoundsRect = new Rectangle((Int32)_position.X, (Int32)_position.Y, _sizeIcon, _sizeIcon);

        }

        public void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>(_textureName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle displayRect = new Rectangle((Int32)_position.X, (Int32)_position.Y, _sizeIcon, _sizeIcon);

            spriteBatch.Begin();
            spriteBatch.Draw(_texture, displayRect, Color.White);
            spriteBatch.End();
        }

        public Boolean IsSelected()
        {
            if (ToolsInterface.isMouseLeftPressed())
                return (ToolsInterface.isMouseIntersects(_position, _towerIconBoundsRect));

            return false;
        }
    }
}
