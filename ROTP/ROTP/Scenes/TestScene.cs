﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ROTP.Scenes.Common;
using Microsoft.Xna.Framework.Input;

namespace ROTP.Scenes
{
    class TestScene : Scene
    {
        Model myModel;
        float aspectRatio;

        Vector3 modelVelocity = Vector3.Zero;

        Vector3 modelPosition = Vector3.Zero;
        float modelRotation = 0.0f;
        Vector3 cameraPosition = new Vector3(0.0f, 50.0f, 5000.0f);


        public TestScene(SceneManager manager)
            : base(manager)
        {
            TransitionOnTime = TimeSpan.FromSeconds(3);
            TransitionOffTime = TimeSpan.FromSeconds(3);
        }

        protected override void LoadContent()
        {
            //Load the test model
            myModel = SceneManager.Game.Content.Load<Model>("Models\\p1_wedge");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public override void Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            base.Update(gameTime, otherSceneHasFocus, coveredByOtherScene);

            if (IsActive)
            {
                modelRotation += (float)gameTime.ElapsedGameTime.TotalMilliseconds * MathHelper.ToRadians(0.1f);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            aspectRatio = SceneManager.GraphicsDevice.Viewport.AspectRatio;

            //Copy any parent transform
            Matrix[] transforms = new Matrix[myModel.Bones.Count];
            myModel.CopyAbsoluteBoneTransformsTo(transforms);

            //Draw the model, a model can have multiple meshes, so loop
            foreach (ModelMesh mesh in myModel.Meshes)
            {
                //this is where the mesh orientation is set
                //as well as our camera and projection
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(modelRotation) * Matrix.CreateTranslation(modelPosition);
                    effect.View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);
                }

                mesh.Draw();
            }

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                SceneManager.Game.Exit();

            updateInput();
        }

        protected void updateInput()
        {
            // Get the game pad state.
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            // Get the Keyboard state
            KeyboardState currentKeyState = Keyboard.GetState();

            Console.WriteLine("updateInput");

            if (currentKeyState.IsKeyDown(Keys.A))
            {
                modelRotation += 0.10f;
            }
            else if (currentKeyState.IsKeyDown(Keys.E))
            {
                modelRotation -= 0.10f;
            }
            else
            {
                // Rotate the model using the left thumbstick, and scale it down
                //modelRotation -= currentState.ThumbSticks.Left.X * 0.10f;
            }

            // Create some velocity if the right trigger is down.
            Vector3 modelVelocityAdd = Vector3.Zero;

            // Find out what direction we should be thrusting, 
            // using rotation.
            modelVelocityAdd.X = -(float)Math.Sin(modelRotation);
            modelVelocityAdd.Z = -(float)Math.Cos(modelRotation);

            if (currentKeyState.IsKeyDown(Keys.Z))
            {
                modelVelocityAdd *= 1.0f;
            }
            else
            {
                // Now scale our direction by how hard the trigger is down.
                //modelVelocityAdd *= currentState.Triggers.Right;
            }

            // Finally, add this vector to our velocity.
            modelVelocity += modelVelocityAdd;

            GamePad.SetVibration(PlayerIndex.One,
                currentState.Triggers.Right,
                currentState.Triggers.Right);


            // In case you get lost, press A (Or Enter) to warp back to the center.
            if (currentState.Buttons.A == ButtonState.Pressed || currentKeyState.IsKeyDown(Keys.Enter))
            {
                modelPosition = Vector3.Zero;
                modelVelocity = Vector3.Zero;
                modelRotation = 0.0f;
            }
        }
    }
}
