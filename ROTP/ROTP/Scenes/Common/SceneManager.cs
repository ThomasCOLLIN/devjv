using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ROTP.Scenes.Common
{
    public class SceneManager : DrawableGameComponent
    {
        private readonly List<Scene> _scenes = new List<Scene>();
        private readonly List<Scene> _scenesToUpdate = new List<Scene>();
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Texture2D _blankTexture;

        public SceneManager(Game game)
            : base(game)
        {
        }

        public void AddScene(Scene scene)
        {
            scene.IsExiting = false;
            Game.Components.Add(scene);
            _scenes.Add(scene);
        }

        public void RemoveScene(Scene scene)
        {
            Game.Components.Remove(scene);
            _scenes.Remove(scene);
            _scenesToUpdate.Remove(scene);
        }

        public void FadeBackBufferToBlack(float alpha)
        {
            Viewport viewport = GraphicsDevice.Viewport;

            _spriteBatch.Begin();
            _spriteBatch.Draw(_blankTexture,
                             new Rectangle(0, 0, viewport.Width, viewport.Height),
                             Color.Black * alpha);
            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
	        _scenesToUpdate.Clear();
 
	        foreach (Scene scene in _scenes)
		        _scenesToUpdate.Add(scene);
 
	        bool othersceneHasFocus = !Game.IsActive;
	        bool coveredByOtherscene = false;
 
	        while (_scenesToUpdate.Count > 0)
	        {
		        Scene scene = _scenesToUpdate[_scenesToUpdate.Count - 1];
		        _scenesToUpdate.RemoveAt(_scenesToUpdate.Count - 1);
		        scene.Update(gameTime, othersceneHasFocus, coveredByOtherscene);
 
		        if (scene.SceneState == SceneState.TransitionOn ||
			        scene.SceneState == SceneState.Active)
		        {
			        // Si c'est la première scène, lui donner l'accès aux entrées utilisateur.
			        if (!othersceneHasFocus)
			        {
				        scene.HandleInput();
				        othersceneHasFocus = true;
			        }
 
			        // Si la scène courant n'est pas un pop-up et est active,
			        // informez les scènes suivantes qu'elles sont recouverte.
			        if (!scene.IsPopup)
				        coveredByOtherscene = true;
		        }
	        }
        }
    }
}
