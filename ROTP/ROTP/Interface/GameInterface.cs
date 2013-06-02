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
        private Vector2 _interfacePosition;
        private Texture2D _interfaceTexture;
        private Rectangle _interfaceBoundsRect;
        private int _screenHeight;
        private ResizeBar _lifeBar;
        private ResizeBar _waveBar;

        private TowerIcon _towerIcon;

        public GameInterface(GraphicsDevice device)
        {
            _screenHeight = device.Viewport.Height;

            _lifeBar = new ResizeBar(new Vector2(0, 0), @"Textures\lifebar", width: 300, maxLife: 400, life: 400);
            _waveBar = new ResizeBar(new Vector2(0, 50), @"Textures\wavebar", width: 300, maxLife: 300, life: 0);
            _towerIcon = new TowerIcon(new Vector2(0, 120), @"Textures\eiffel-tower-icon");
        }

        public void Load(ContentManager content)
        {
            _interfaceTexture = content.Load<Texture2D>(@"Textures\interface");
            _lifeBar.Load(content);
            _waveBar.Load(content);
            _towerIcon.Load(content);
        }

        public void Update()
        {
        }

        public void HandleInput()
        {
            _interfaceBoundsRect = new Rectangle((Int32)_interfacePosition.X, (Int32)_interfacePosition.Y, _interfaceTexture.Width, _interfaceTexture.Height);

            //if (ToolsInterface.isMouseLeftPressed())
            //{
            //    if (ToolsInterface.isMouseIntersects(_interfacePosition, _interfaceBoundsRect))
            //    {
            //        _lifeBar.ChangeLife(-1);
            //        _waveBar.ChangeLife(1);
            //    }
            //}
            _towerIcon.HandleInput();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _interfacePosition = new Vector2(0, _screenHeight - _interfaceTexture.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(_interfaceTexture, _interfacePosition, Color.White);
            spriteBatch.End();
            //

            _lifeBar.Draw(spriteBatch);
            _waveBar.Draw(spriteBatch);
            _towerIcon.Draw(spriteBatch);
        }

    #region private
        

    #endregion private
    }
}
