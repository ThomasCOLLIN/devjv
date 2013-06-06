using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ROTP.Scenes.Common;
using ROTP.Elements;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using ROTP.Scenes.Characters;
using ROTP.Scenes.Menus;

namespace ROTP.Scenes
{
    class GameScene : Scene
    {
        Background gameBackground;
        GameInterface gameInterface;
        public Dictionary<string, Model> meshModels;
        List<Mob> mobs;

        public GameScene(SceneManager manager)
            : base(manager)
        {
            meshModels = new Dictionary<string, Model>();
            mobs = new List<Mob>();
        }

        protected override void LoadContent()
        {
            gameBackground = new Background(SceneManager.GraphicsDevice);
            gameBackground.LoadContent(SceneManager.Game.Content);
            gameInterface = new GameInterface(SceneManager.GraphicsDevice);
            gameInterface.Load(SceneManager.Game.Content);

            meshModels.Add("testchar", SceneManager.Game.Content.Load<Model>("Models\\p1_wedge"));

            //just a test erase it
            mobs.Add(new Mob(meshModels["testchar"]));
        }

        protected override void UnloadContent()
        {
            gameBackground.UnloadContent();
        }

        public override void Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            base.Update(gameTime, otherSceneHasFocus, coveredByOtherScene);

            if (IsActive)
            {
                gameBackground.Update();
                gameInterface.Update();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            gameBackground.Draw();
            foreach (Mob mob in mobs)
            {
                mob.draw();
            }
            gameInterface.Draw(SceneManager.SpriteBatch);

            if (TransitionPosition > 0)
            {
                SceneManager.FadeBackBufferToBlack(1f - TransitionAlpha);

                GraphicsDevice.BlendState = BlendState.Opaque;
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            }
        }

        public override void HandleInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                new MenuPause(SceneManager, this).Add();
                return;
            }

            gameBackground.HandleInput();
            gameInterface.HandleInput();
        }
    }
}
