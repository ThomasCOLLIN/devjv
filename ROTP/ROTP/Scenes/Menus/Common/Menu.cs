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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SceneManager.SpriteBatch.Begin();
            var transitionOffset = (float)Math.Pow(TransitionPosition, 2);
            var titlePosition = new Vector2(GraphicsDevice.Viewport.Width / 2f, 80);
            Vector2 titleOrigin = SceneManager.Font.MeasureString(_menuTitle) / 2;
            Color titleColor = new Color(192, 192, 192) * TransitionAlpha;
            titlePosition.X -= transitionOffset * 100;
            SceneManager.SpriteBatch.DrawString(SceneManager.Font, _menuTitle, titlePosition, titleColor, 0, titleOrigin, 1, SpriteEffects.None, 0);


            for (int i = 0; i < _menuItems.Count; i++)
            {
                MenuItem item = _menuItems[i];
                Color color = i == _selectedIndex ? Color.Black : Color.White;
                titlePosition.Y += 20;
                SceneManager.SpriteBatch.DrawString(SceneManager.Font, item.Text, titlePosition, color, 0, titleOrigin, 1, SpriteEffects.None, 0);
            }
            SceneManager.SpriteBatch.End();

            if (TransitionPosition > 0)
            {
                // de base transparent vers noir utiliser (1f - TransitionAlpha) -> noir vers transparent
                SceneManager.FadeBackBufferToBlack(1f - TransitionAlpha);

                // see http://blogs.msdn.com/b/shawnhar/archive/2010/06/18/spritebatch-and-renderstates-in-xna-game-studio-4-0.aspx
                GraphicsDevice.BlendState = BlendState.Opaque;
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            }
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
