using System;
using Microsoft.Xna.Framework;
using ROTP.Scenes.Common;
using Microsoft.Xna.Framework.Graphics;

namespace ROTP.Scenes.Menus.Common
{
    class MenuItem
    {
        private const float Scale = 0.8f;

        private string _text;
        private float _selectionFade;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public event EventHandler Selected;

        
        public MenuItem(string text)
        {
            _text = text;
        }

        public void Update(GameTime gametime, bool isSelected)
        {
            float fadeSpeed = (float) gametime.ElapsedGameTime.TotalSeconds * 4;

            _selectionFade = isSelected
                ? Math.Min(_selectionFade + fadeSpeed, 1)
                : Math.Max(_selectionFade - fadeSpeed, 0);
        }


        // crade : le menu devrait set lui meme la position general des items
        public void Draw(GameTime gametime, bool isSelected, Menu menu, float offset, float posY)
        {
            SceneManager sm = menu.SceneManager;
            float posx = sm.GraphicsDevice.Viewport.Width / 6f;
            Vector2 itemPosition = new Vector2(posx, posY);
            Vector2 itemSize = Menu.ItemFont.MeasureString(_text);
            Vector2 origin = new Vector2(0, GetItemHeight() / 2f);
            double time = gametime.TotalGameTime.TotalSeconds;
            float pulsate = menu.SceneState == SceneState.Active ? (float)Math.Sin(time * 6) + Scale : 1;
            float scale = Scale + pulsate * 0.05f * _selectionFade;

            Color color = (isSelected ? Color.Black : Color.White) * menu.TransitionAlpha;
            itemPosition.X -= offset * (posx + Menu.ItemFont.MeasureString(_text).X);
            sm.SpriteBatch.DrawString(Menu.ItemFont, _text, itemPosition, color, 0, origin, scale, SpriteEffects.None, 0);
        }

        public void OnItemSelected()
        {
            if (Selected != null)
                Selected(this, new EventArgs());
        }

        public int GetItemHeight()
        {
            return (int)(Menu.ItemFont.LineSpacing * Scale);
        }

    }
}
