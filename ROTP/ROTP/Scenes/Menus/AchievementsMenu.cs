using ROTP.Scenes.Menus.Common;
using ROTP.Achievements.Common;
using ROTP.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace ROTP.Scenes.Common
{
    class AchievementsMenu : Scene
    {
        private const string _title = "Succes";
        private const float Scale = 0.8f;

        private int _indexX = 0;
        private int _indexY = 0;

        private float _padding;
        private int _elementPerRow;
        private float _margin;
        private int _numberOfRow = 0;
        private List<List<DrawableAchievement>> achievements;



        private bool hasResetFocus = false;
        private float _resetFade;

        private bool hasCancelFocus = false;
        private float _cancelFade;

        public AchievementsMenu(SceneManager manager)
            : base(manager)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(1);
        }

        public override void  Initialize()
        {
            int screenWidth = SceneManager.GraphicsDevice.Viewport.Width;
            _padding = screenWidth / 6f;
            _elementPerRow = (int) ((screenWidth - 2 * _padding) / DrawableAchievement.ImageSize);
            _margin = (((screenWidth - 2 * _padding) % DrawableAchievement.ImageSize) / (float) (_elementPerRow - 1));

            InitAchievements();
            base.Initialize();
        }

        private void InitAchievements()
        {
            achievements = new List<List<DrawableAchievement>>();
            List<DrawableAchievement> currentRow = new List<DrawableAchievement>();
            achievements.Add(currentRow);
            _numberOfRow++;

            foreach (Achievement item in AchievementManager.Instance.GetAll())
	        {
                if (currentRow.Count >= _elementPerRow)
                {
                    currentRow = new List<DrawableAchievement>();
                    achievements.Add(currentRow);
                    _numberOfRow++;
                }

                currentRow.Add(new DrawableAchievement(SceneManager, item));
            }
        }

        protected override void LoadContent()
        {
            LoadAchivement();
        }

        private void LoadAchivement()
        {
            foreach (List<DrawableAchievement> row in achievements)
            {
                foreach (DrawableAchievement achievement in row)
                    achievement.Load();
            }
        }

        public override void  Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
 	        base.Update(gameTime, otherSceneHasFocus, coveredByOtherScene);
            float fadeSpeed = (float) gameTime.ElapsedGameTime.TotalSeconds * 4;

            _resetFade = hasResetFocus
                ? Math.Min(_resetFade + fadeSpeed, 1)
                : Math.Max(_resetFade - fadeSpeed, 0);

            _cancelFade = hasCancelFocus
                ? Math.Min(_cancelFade + fadeSpeed, 1)
                : Math.Max(_cancelFade - fadeSpeed, 0);
        }

        public override void HandleInput()
        {
            if (MenuInput.IsUpPressed())
            {
                if (hasCancelFocus)
                {
                    hasCancelFocus = false;
                    hasResetFocus = true;
                }
                else if (hasResetFocus)
                {
                    hasResetFocus = false;
                    _indexX = _numberOfRow - 1;
                    _indexY = Math.Min(achievements[_numberOfRow - 1].Count, achievements[0].Count) - 1;

                }
                else if (_indexX <= 0)
                {
                    hasCancelFocus = true;
                }
                else
                {
                    _indexX--;
                    _indexY = Math.Min(achievements[_indexX].Count, achievements[_indexX + 1].Count) - 1;
                }
            }

            if (MenuInput.IsDownPressed())
            {
                if (hasCancelFocus)
                {
                    hasCancelFocus = false;
                    _indexX = 0;
                    _indexY = Math.Min(achievements[0].Count, achievements[_numberOfRow - 1].Count) - 1;
                }
                else if (hasResetFocus)
                {
                    hasResetFocus = false;
                    hasCancelFocus = true;
                }
                else if (_indexX >= _numberOfRow - 1)
                {
                    hasResetFocus = true;
                }
                else
                {
                    _indexX++;
                    _indexY = Math.Min(achievements[_indexX].Count, achievements[_indexX - 1].Count) - 1;
                }
            }

            if (MenuInput.IsLeftPressed())
            {
                if (!hasCancelFocus && !hasResetFocus)
                    _indexY = _indexY > 0 ? _indexY - 1 : achievements[_indexX].Count - 1;
            }

            if (MenuInput.IsRightPressed())
            {
                if (!hasCancelFocus && !hasResetFocus)
                    _indexY = _indexY < achievements[_indexX].Count - 1 ? _indexY + 1 : 0;
            }

            if (MenuInput.IsSelectPressed())
            {
                if (hasResetFocus)
                    OnResetSelected();
                else if (hasCancelFocus)
                    OnCancelSelected();
            }

            if (MenuInput.IsCancelPressed())
                OnCancelSelected();
        }

        public override void Draw(GameTime gameTime)
        {
            SceneManager.SpriteBatch.Begin();

            var transitionOffset = (float)Math.Pow(TransitionPosition, 2);
            var titlePosition = new Vector2(GraphicsDevice.Viewport.Width / 6f, 80);
            Color titleColor = new Color(192, 192, 192) * TransitionAlpha;
            titlePosition.X -= transitionOffset * 100;
            SceneManager.SpriteBatch.DrawString(Menu.TitleFont, "Succes", titlePosition, titleColor, 0, new Vector2(), 1, SpriteEffects.None, 0);

            float posY = Math.Max(SceneManager.GraphicsDevice.Viewport.Height / 3f, 90);

            posY = DrawAchievements(gameTime, posY);
            DrawDescription(posY);
            DrawButtons(gameTime, posY);

            SceneManager.SpriteBatch.End();
        }

        private float DrawAchievements(GameTime gameTime, float posy)
        {
            bool isEndOfLine = true;
            for (int i = 0; i < achievements.Count; i++)
			{
                float offset = (float)Math.Pow(TransitionPosition, 2);

                float posx = _padding;

                for (int j = 0; j < achievements[i].Count; j++)
			    {
                    isEndOfLine = false;
                    posx -= offset * (posx + DrawableAchievement.ImageSize);
			        achievements[i][j].Draw(SceneManager, new Vector2(posx, posy), !hasCancelFocus && !hasResetFocus && i == _indexX && j == _indexY);
                    posx += achievements[i][j].Image.Bounds.Width + _margin;

                    if (j == achievements[i].Count - 1)
                    {
                        isEndOfLine = true;
                        posy += achievements[i][j].Image.Bounds.Height + _margin;
                    }
                }
			}

            return posy + _margin + (isEndOfLine ? 0 : DrawableAchievement.ImageSize);
        }

        private void DrawDescription(float posy)
        {
            if (hasCancelFocus || hasResetFocus || SceneState != SceneState.Active)
                return;

            DrawableAchievement selectedItem = achievements[_indexX][_indexY];
            float posx = GraphicsDevice.Viewport.Width - GraphicsDevice.Viewport.Width / 6f;
            posx -= GraphicsDevice.Viewport.Width / 4f;
            SceneManager.SpriteBatch.DrawString(Menu.TitleFont, selectedItem.Title, new Vector2(posx, posy), Color.Black);

            posy += Menu.TitleFont.LineSpacing + 10;
            SceneManager.SpriteBatch.DrawString(Menu.ItemFont, selectedItem.IsOwned ? selectedItem.Description : "??????????????????", new Vector2(posx, posy), Color.Black);
        }

        //  crade : pas le temps
        private void DrawButtons(GameTime gametime, float posy)
        {
            //common
            int linespacing = (int)(Menu.ItemFont.LineSpacing * Scale);
            float posx = SceneManager.GraphicsDevice.Viewport.Width / 6f;
            float offset = (float)Math.Pow(TransitionPosition, 2);
            double time = gametime.TotalGameTime.TotalSeconds;
            float pulsate = SceneState == SceneState.Active ? (float)Math.Sin(time * 6) + Scale : 1;
            Vector2 origin = new Vector2(0, linespacing / 2f);

            //reset button
            String text = "Reinitialiser";
            Vector2 itemPosition = new Vector2(posx, posy);
            float scale = Scale + pulsate * 0.05f * _resetFade;
            Color color = (hasResetFocus ? Color.Black : Color.White) * TransitionAlpha;
            itemPosition.X -= offset * (posx + Menu.ItemFont.MeasureString(text).X);
            SceneManager.SpriteBatch.DrawString(Menu.ItemFont, text, itemPosition, color, 0, origin, scale, SpriteEffects.None, 0);

            // cancel button
            text = "Retour";
            itemPosition.Y += linespacing + 15;
            itemPosition.X -= offset * (posx + Menu.ItemFont.MeasureString(text).X);
            color = (hasCancelFocus ? Color.Black : Color.White) * TransitionAlpha;
            scale = Scale + pulsate * 0.05f * _cancelFade;
            SceneManager.SpriteBatch.DrawString(Menu.ItemFont, text, itemPosition, color, 0, origin, scale, SpriteEffects.None, 0);
        }

        public void OnCancelSelected()
        {
            Remove();
        }

        public void OnResetSelected()
        {
            //TODO : add popup
            AchievementManager.Instance.ResetAchievements();
            InitAchievements();
            LoadAchivement();
        }
    }
}
