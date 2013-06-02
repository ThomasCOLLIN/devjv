using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace ROTP.Interface
{
    public class TowerInformationsBox
    {
        public String Text { get; set; }
        private SpriteFont _font;
        private Vector2 _txtPos;
        private DateTime _timer;

        public TowerInformationsBox()
        {
            _timer = DateTime.Now;
        }

        public void Display()
        {
            _timer = DateTime.Now.AddMilliseconds(1500);
        }

        public void Load(ContentManager content, Vector2 txtPosition)
        {
            _font = content.Load<SpriteFont>(@"Texts\TestText");
            _txtPos = txtPosition;
        }

        public void Draw(SpriteBatch sb)
        {
            if (_timer > DateTime.Now)
            {
                sb.Begin();
                sb.DrawString(_font, Text, _txtPos, Color.DeepPink);
                sb.End();
            }
        }

    }
}
