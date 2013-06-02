using System.Collections.Generic;
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

        #region getter / setter
        public List<Scene> Scenes
        {
            get { return _scenes; }
        }

        public List<Scene> ScenesToUpdate
        {
            get { return _scenesToUpdate; }
        } 

        public SpriteBatch SpriteBatch
        {
            get { return _spriteBatch; }
            set { _spriteBatch = value; }
        }

        public SpriteFont Font
        {
            get { return _font; }
            set { _font = value; }
        }

        public Texture2D BlankTexture
        {
            get { return _blankTexture; }
            set { _blankTexture = value; }
        }
        #endregion

        public SceneManager(Game game)
            : base(game)
        {
            Game.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _blankTexture = Game.Content.Load<Texture2D>("Textures/blank");
            _font = Game.Content.Load<SpriteFont>("Texts/TestText");
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
			        if (!othersceneHasFocus)
			        {
				        scene.HandleInput();
				        othersceneHasFocus = true;
			        }

                    if (!scene.IsPopup)
				        coveredByOtherscene = true;
		        }
	        }
        }


        #region scene management
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
        #endregion
    }
}
