using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ROTP.Interface;

namespace ROTP
{
    public class GameInterface
    {
        private int _screenWidth;
        private int _screenHeight;

        private Vector2 _backgroundPosition;
        private Texture2D _backgroundTexture;

        private ResizeBar _lifeBar;
        private ResizeBar _waveBar;

        private TowerIcon _towerIcon;

        public GameInterface(GraphicsDeviceManager graphics)
        {
            _screenWidth = graphics.PreferredBackBufferWidth;
            _screenHeight = graphics.PreferredBackBufferHeight;

            _lifeBar = new ResizeBar(new Vector2(0, 0), @"Textures\lifebar", width: 300, maxLife: 400, life: 400);
            _waveBar = new ResizeBar(new Vector2(0, 50), @"Textures\wavebar", width: 300, maxLife: 300, life: 0);
            _towerIcon = new TowerIcon(new Vector2(0, 120), @"Textures\eiffel-tower-icon");
        }

        public void Load(ContentManager content)
        {
            _backgroundTexture = content.Load<Texture2D>(@"Textures\interface");
            _lifeBar.Load(content);
            _waveBar.Load(content);
            _towerIcon.Load(content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _backgroundPosition = new Vector2(0, _screenHeight - _backgroundTexture.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(_backgroundTexture, _backgroundPosition, Color.White);
            spriteBatch.End();

            _lifeBar.Draw(spriteBatch);
            _waveBar.Draw(spriteBatch);
            _towerIcon.Draw(spriteBatch);
        }

        public void Update(Game game, MouseState mouseState)
        {
            if (InterfaceTools.isMouseLeftPressed(mouseState))
            {
                if (InterfaceTools.isMouseIntersects(mouseState, _backgroundPosition, _backgroundTexture))
                {
                    //game.Exit();
                    _lifeBar.ChangeLife(-1);
                    _waveBar.ChangeLife(1);
                }
            }


        }

    #region private
        

    #endregion private
    }
}
