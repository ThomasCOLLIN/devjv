using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ROTP.Scenes.Common;
using ROTP.Elements;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using ROTP.Scenes.Characters;
using ROTP.Scenes.Menus;
using ROTP.Utils;
using System;
using System.Diagnostics;

namespace ROTP.Scenes
{
    class GameScene : Scene
    {
        Background gameBackground;
        GameInterface gameInterface;
        MapCase testCase;

        public GameScene(SceneManager manager)
            : base(manager)
        {
            GlobalsVar.MeshModels = new Dictionary<string, Model>();
            GlobalsVar.mobs = new List<Mob>();
        }

        protected override void LoadContent()
        {
            GlobalsVar.Camera = new Camera(SceneManager.GraphicsDevice.Viewport);
            gameBackground = new Background(SceneManager.GraphicsDevice);
            gameBackground.LoadContent(SceneManager.Game.Content);
            gameInterface = new GameInterface(SceneManager.GraphicsDevice);
            gameInterface.Load(SceneManager.Game.Content);
            
            GlobalsVar.Map = new Map();
            GlobalsVar.MeshModels.Add("testchar", SceneManager.Game.Content.Load<Model>("Models\\p1_wedge"));
            GlobalsVar.MeshModels.Add("grassGround", SceneManager.Game.Content.Load<Model>("Models\\GrassSquare"));

            GlobalsVar.Map.Generate(10, 10, "grass");
        }

        protected override void UnloadContent()
        {
            gameBackground.UnloadContent();
        }



        public override void Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            base.Update(gameTime, otherSceneHasFocus, coveredByOtherScene);
            GlobalsVar.Camera.Update();

            if (IsActive)
            {
                gameBackground.Update();
                GlobalsVar.Map.Update(gameTime);
                foreach (Mob mob in GlobalsVar.mobs)
                {
                    mob.Update(gameTime);
                }
                CheckMouse();
                gameInterface.Update();
            }


        }

        public void CheckMouse()
        {
            MouseState mouseState = Mouse.GetState();
            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;

            //Console.WriteLine("Mouse state : " + mouseState.X + "X." + mouseState.Y + "Y");

            /*Vector3 nearsource = new Vector3((float)mouseX, (float)mouseY, 0f);
            Vector3 farsource = new Vector3((float)mouseX, (float)mouseY, 1f);
            Matrix world = Matrix.CreateTranslation(0, 0, 0);

            Vector3 nearPoint = graphics.GraphicsDevice.Viewport.Unproject(nearsource, proj, view, world);

            Vector3 farPoint = graphics.GraphicsDevice.Viewport.Unproject(farsource, proj, view, world);*/
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            gameBackground.Draw();

            //Console.WriteLine("map is " + GlobalsVar.Map.MapArray.Count + " " + GlobalsVar.Map.MapArray[GlobalsVar.Map.MapArray.Count - 1].Count);
            GlobalsVar.Map.Draw();
            foreach (Mob mob in GlobalsVar.mobs)
            {
                mob.Draw();
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
