using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ROTP.Scenes.Common;
using ROTP.Elements;
using Microsoft.Xna.Framework.Input;

namespace ROTP.Scenes
{
    class GameScene : Scene
    {
        Background gameBackground;
        GameInterface gameInterface;


        public GameScene(SceneManager manager)
            : base(manager)
        {
        }

        protected override void LoadContent()
        {
            gameBackground = new Background(SceneManager.GraphicsDevice);
            gameBackground.LoadContent(SceneManager.Game.Content);
            gameInterface = new GameInterface(SceneManager.GraphicsDevice);
            gameInterface.Load(SceneManager.Game.Content);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                SceneManager.Game.Exit();

            gameBackground.HandleInput();
            gameInterface.HandleInput();
        }
    }
}
