using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ROTP.Scenes.Common;
using ROTP.Elements;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using ROTP.Characters;
using ROTP.Scenes.Menus;
using ROTP.Utils;
using System;
using System.Diagnostics;
using ROTP.Characters.Towers;
using ROTP.Characters.Mobs;

namespace ROTP.Scenes
{
    class GameScene : Scene
    {
        Background gameBackground;
        GameInterface gameInterface;
        MapCase testCase;

        List<Tower> towers;


        public GameScene(SceneManager manager)
            : base(manager)
        {
            GlobalsVar.MeshModels = new Dictionary<string, Model>();
            GlobalsVar.Mobs = new List<Mob>();
        }

        protected override void LoadContent()
        {
            GlobalsVar.Camera = new Camera(SceneManager.GraphicsDevice.Viewport);
            gameBackground = new Background(SceneManager.GraphicsDevice);
            gameBackground.LoadContent(SceneManager.Game.Content);
            gameInterface = new GameInterface(SceneManager.GraphicsDevice);
            gameInterface.Load(SceneManager.Game.Content);

            GlobalsVar.Map = new Map();
            GlobalsVar.MeshModels.Add("testchar", SceneManager.Game.Content.Load<Model>(@"Models\p1_wedge"));
            GlobalsVar.MeshModels.Add("grassGround", SceneManager.Game.Content.Load<Model>(@"Models\GrassSquare"));
            GlobalsVar.MeshModels.Add("totem", SceneManager.Game.Content.Load<Model>(@"Models\totem-1"));
            GlobalsVar.MeshModels.Add("fireTower", SceneManager.Game.Content.Load<Model>(@"Models\fire_tower"));
            GlobalsVar.MeshModels.Add("waterTower", SceneManager.Game.Content.Load<Model>(@"Models\water_tower"));

            GlobalsVar.MeshModels.Add("mob1", SceneManager.Game.Content.Load<Model>(@"Models\mob-1"));

            GlobalsVar.Map.Generate(10, 10, "grass");

            towers = new List<Tower>();

            GlobalsVar.Mobs.Add(new TestMob(new Vector3(0, 2*5, 0)));
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
                foreach (Mob mob in GlobalsVar.Mobs)
                {
                    mob.Update(gameTime);
                }
                gameInterface.Update();
            }
        }

        public Vector2? CheckMouse()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Console.WriteLine("He clicked with left mouse button !");

                int mouseX = mouseState.X;
                int mouseY = mouseState.Y;

                //Console.WriteLine("Mouse state : " + mouseState.X + "X." + mouseState.Y + "Y");

                Vector3 nearsource = new Vector3((float)mouseX, (float)mouseY, 0f);
                Vector3 farsource = new Vector3((float)mouseX, (float)mouseY, 1f);

                Matrix world = Matrix.CreateTranslation(0, 0, 0);
                //Matrix world = Matrix.Identity;

                Vector3 nearPoint = SceneManager.GraphicsDevice.Viewport.Unproject(nearsource, GlobalsVar.Camera.ProjectionMatrix, GlobalsVar.Camera.ViewMatrix, world);
                Vector3 farPoint = SceneManager.GraphicsDevice.Viewport.Unproject(farsource, GlobalsVar.Camera.ProjectionMatrix, GlobalsVar.Camera.ViewMatrix, world);

                Vector3 direction = farPoint - nearPoint;
                direction.Normalize();
                Ray pickRay = new Ray(nearPoint, direction);

                float selectedDistance = float.MaxValue;
                MapCase res = null;

                foreach (List<MapCase> listCase in GlobalsVar.Map.MapArray)
                {
                    foreach (MapCase mapcase in listCase)
                    {
                        foreach (ModelMesh mesh in mapcase.MapModel.Meshes)
                        {
                            BoundingSphere sphere = mesh.BoundingSphere.Transform(Matrix.CreateScale(mapcase.Ratio) * Matrix.CreateTranslation(mapcase.ModelPosition));
                            //sphere.Center = new Vector3(mapcase.ModelPosition.X, mapcase.ModelPosition.Y, mapcase.ModelPosition.Z);
                            Nullable<float> result = pickRay.Intersects(sphere);
                            if (result.HasValue)
                            {
                                //Console.WriteLine("For item at " + mapcase.ModelPosition.X / 5 + " " + mapcase.ModelPosition.Y / 5 + " Distance is " + result.Value);
                                if (result.Value < selectedDistance)
                                {
                                    selectedDistance = result.Value;
                                    res = mapcase;
                                }
                            }
                            else
                            {
                                //Console.WriteLine("Is null at " + mapcase.ModelPosition.X / 5 + " " + mapcase.ModelPosition.Y / 5);
                            }

                            if (res != null)
                            {
                               Console.WriteLine("You cliked on item that is in coordinates : " + res.ModelPosition.X / 5 + " " + res.ModelPosition.Y / 5);
                               return new Vector2(res.ModelPosition.X / 5, res.ModelPosition.Y / 5);
                            }
                        }
                    }
                }
            }

            return null;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            gameBackground.Draw();

            //Console.WriteLine("map is " + GlobalsVar.Map.MapArray.Count + " " + GlobalsVar.Map.MapArray[GlobalsVar.Map.MapArray.Count - 1].Count);
            GlobalsVar.Map.Draw();
            foreach (Mob mob in GlobalsVar.Mobs)
            {
                mob.Draw(gameTime);
            }

            towers.Sort(Comparers.TowerCompareY);
            foreach (Tower tower in towers)
            {   
                tower.Draw(gameTime);
            }


            // Draw the Interface. Must be the last thing to draw in the scene. (Except menu).
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

            Vector2? clickObject = CheckMouse();
            if (clickObject != null)
            {
                towers.Add(TowerFactory.GenerateTower(gameInterface.TowerSelectedType, new Vector3(clickObject.Value.X * 5, clickObject.Value.Y * 5, 0)));
            }

            gameBackground.HandleInput();
            gameInterface.HandleInput();
        }
    }
}
