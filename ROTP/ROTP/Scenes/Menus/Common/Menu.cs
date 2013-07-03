using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ROTP.Scenes.Common;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using ROTP.Input;

namespace ROTP.Scenes.Menus.Common
{
    class Menu : Scene
    {
        private int _selectedIndex;
        private List<MenuItem> _menuItems;
        private string _menuTitle;

        protected List<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set { _menuItems = value; }
        }

        public Menu(SceneManager manager, string title)
            : base(manager)
        {
            _menuTitle = title;
            _menuItems = new List<MenuItem>();

            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(1);
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
        }

        public override void HandleInput()
        {
            if (MenuInput.IsUpPressed())
                _selectedIndex = --_selectedIndex < 0 ? _menuItems.Count - 1 : _selectedIndex;

            if (MenuInput.IsDownPressed())
                _selectedIndex = ++_selectedIndex >= _menuItems.Count ? 0 : _selectedIndex;

            if (MenuInput.IsSelectPressed())
                OnSelect();

            if (MenuInput.IsCancelPressed())
                OnCancel();
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 origin = new Vector2();

            float posx = GraphicsDevice.Viewport.Width / 6f;
            SceneManager.SpriteBatch.Begin();
            var transitionOffset = (float)Math.Pow(TransitionPosition, 2);
            var titlePosition = new Vector2(posx, 80);
            Color titleColor = new Color(192, 192, 192) * TransitionAlpha;
            titlePosition.X -= transitionOffset * 100;
            SceneManager.SpriteBatch.DrawString(SceneManager.Font, _menuTitle, titlePosition, titleColor, 0, origin, 1, SpriteEffects.None, 0);

            Vector2 itemPosition = new Vector2(posx, Math.Max(GraphicsDevice.Viewport.Height / 3f, 90));
            Vector2 itemSize = SceneManager.Font.MeasureString(_menuTitle);
            itemPosition.Y -= itemSize.Y / 2f;
            for (int i = 0; i < _menuItems.Count; i++)
            {
                MenuItem item = _menuItems[i];
                Color color = i == _selectedIndex ? Color.Black : Color.White;
                itemPosition.X -= transitionOffset * (posx + SceneManager.Font.MeasureString(item.Text).X);
                SceneManager.SpriteBatch.DrawString(SceneManager.Font, item.Text, itemPosition, color, 0, origin, 1, SpriteEffects.None, 0);
                itemPosition.Y += itemSize.Y;
            }
            SceneManager.SpriteBatch.End();
        }

        public virtual void OnCancel()
        {
            Remove();
        }

        public virtual void OnSelect()
        {
            if (_selectedIndex >= 0 && _selectedIndex < _menuItems.Count)
                _menuItems[_selectedIndex].OnItemSelected();
        }
    }
}
