using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ROTP.Scenes.Common;
using Microsoft.Xna.Framework.Graphics;

namespace ROTP.Achievements.Common
{
    class DrawableAchievement
    {
        private String _title;
        private String _description;
        private String _imagePath;
        private bool _isOwned;
        private SceneManager _sceneManager;
        private Texture2D _image;

        public string Title
        {
            get { return _title; }
        }

        public string Description
        {
            get { return _description; }
        }

        public bool IsOwned
        {
            get { return _isOwned; } 
        }

        public Texture2D Image
        {
            get { return _image; }
        }

        public DrawableAchievement(SceneManager sceneManager, Achievement achievement)
        {
            _title = achievement.Name;
            _description = achievement.Description;
            _imagePath = achievement.ImagePath;
            _isOwned = achievement.IsOwned;
            _sceneManager = sceneManager;
        }

        public void Load()
        {
            _image = _sceneManager.Game.Content.Load<Texture2D>(_imagePath);
        }

        public void Draw(SceneManager sm, Vector2 position, bool isSelected)
        {
            Rectangle imageRect = new Rectangle((int)position.X, (int)position.Y, 151, 151);
            _sceneManager.SpriteBatch.Draw(_image, imageRect, Color.White * (isSelected ? 1 : 0.5f));
        }
    }
}
