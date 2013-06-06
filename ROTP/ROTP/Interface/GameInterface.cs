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

        private List<TowerIcon> _towerIcons;
        private TowerInformationsBox _towerInformationBox;

        private Texture2D _tempLifeButton;
        private Vector2 _lifeButtonPosition = new Vector2(160, 650);
        private Rectangle _lifeButtonBoundsRect;


        public GameInterface(GraphicsDevice device)
        {
            _screenHeight = device.Viewport.Height;

            _lifeBar = new ResizeBar(new Vector2(250, 650), @"Textures\lifebar", width: 300, maxLife: 400, life: 400);
            _waveBar = new ResizeBar(new Vector2(250, 680), @"Textures\wavebar", width: 300, maxLife: 300, life: 0);


            _towerIcons = new List<TowerIcon>();
           
            TowerIcon towerIcon = new TowerIcon(new Vector2(10, 650), @"Textures\button-eiffel-tower");
            towerIcon.Text = "Toto\nThis is Sparta !";
            _towerIcons.Add(towerIcon);


            towerIcon = new TowerIcon(new Vector2(60, 650), @"Textures\button-eiffel-tower");
            towerIcon.Text = "Titi";
            _towerIcons.Add(towerIcon);

            towerIcon = new TowerIcon(new Vector2(110, 650), @"Textures\button-eiffel-tower");
            towerIcon.Text = "Tata";
            _towerIcons.Add(towerIcon);


            _towerInformationBox = new TowerInformationsBox();
        }

        public void Load(ContentManager content)
        {
            _interfaceTexture = content.Load<Texture2D>(@"Textures\interface");
            _lifeBar.Load(content);
            _waveBar.Load(content);

            foreach (var towerIcon in _towerIcons)
            {
                towerIcon.Load(content);    
            }

            _towerInformationBox.Load(content, new Vector2(700, 650));

            _tempLifeButton = content.Load<Texture2D>(@"Textures\button-eiffel-tower");
            _lifeButtonBoundsRect = new Rectangle((Int32)_lifeButtonPosition.X, (Int32)_lifeButtonPosition.Y, 45, 45);

        }

        public void Update()
        {
        }

        public void HandleInput()
        {
            _interfaceBoundsRect = new Rectangle((Int32)_interfacePosition.X, (Int32)_interfacePosition.Y, _interfaceTexture.Width, _interfaceTexture.Height);          

            foreach (var towerIcon in _towerIcons)
            {
                if (towerIcon.IsSelected())
                {
                    _towerInformationBox.Display();
                    _towerInformationBox.Text = towerIcon.Text;
                    break;
                }
            }

            if (ToolsInterface.isMouseLeftPressed())
            {
                if (ToolsInterface.isMouseIntersects(_lifeButtonPosition, _lifeButtonBoundsRect))
                {
                    _lifeBar.ChangeLife(-1);
                    _waveBar.ChangeLife(1);
                }
            }
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
            foreach (var towerIcon in _towerIcons)
            {
                towerIcon.Draw(spriteBatch);    
            }
            _towerInformationBox.Draw(spriteBatch);


            Rectangle displayRect = new Rectangle((Int32)_lifeButtonPosition.X, (Int32)_lifeButtonPosition.Y, 45, 45);
            spriteBatch.Begin();
            spriteBatch.Draw(_tempLifeButton, displayRect, Color.White);
            spriteBatch.End();
        }

    #region private
        

    #endregion private
    }
}
