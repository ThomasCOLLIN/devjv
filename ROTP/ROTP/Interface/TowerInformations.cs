using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace ROTP.Interface
{
    public class TowerInformations
    {
        private String _text;
        private SpriteFont _font;
        private Vector2 _txtPos;

        public TowerInformations(ContentManager content, Vector2 txtPosition)
        {
            _font = content.Load<SpriteFont>("TestText");
            _txtPos = txtPosition;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(_font, _text, _txtPos, Color.Pink);
        }

    }
}
