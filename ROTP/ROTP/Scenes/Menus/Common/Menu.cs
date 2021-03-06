﻿using System;
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

        private static SpriteFont _titleFont = null;

        private static SpriteFont _itemFont = null;

        public static SpriteFont ItemFont
        {
            get { return Menu._itemFont; }
        }

        public static SpriteFont TitleFont
        {
            get { return Menu._titleFont; }
        }
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
            if (_titleFont == null)
                _titleFont = SceneManager.Game.Content.Load<SpriteFont>("Texts/MenuTitle");
            if (_itemFont == null)
                _itemFont = SceneManager.Game.Content.Load<SpriteFont>("Texts/MenuItem");
        }

        protected override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            base.Update(gameTime, otherSceneHasFocus, coveredByOtherScene);

            for (int i = 0; i < _menuItems.Count; i++)
                _menuItems[i].Update(gameTime, IsActive && (i == _selectedIndex));

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
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);
            Vector2 titlePosition = new Vector2(posx, 80);
            Color titleColor = new Color(192, 192, 192) * TransitionAlpha;
            titlePosition.X -= transitionOffset * 100;
            SceneManager.SpriteBatch.DrawString(_titleFont, _menuTitle, titlePosition, titleColor, 0, origin, 1, SpriteEffects.None, 0);

            float posY = Math.Max(SceneManager.GraphicsDevice.Viewport.Height / 3f, 90);
            for (int i = 0; i < _menuItems.Count; i++)
            {
                _menuItems[i].Draw(gameTime, IsActive && (i == _selectedIndex), this, transitionOffset, posY);
                posY += _menuItems[i].GetItemHeight() + 15;
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
