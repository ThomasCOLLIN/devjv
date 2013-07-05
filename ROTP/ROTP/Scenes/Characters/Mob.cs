using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ROTP.Utils;

namespace ROTP.Scenes.Characters
{
    class Mob
    {
        private Model mobModel;
        //Vector3 modelVelocity;
        Vector3 modelPosition;
        Matrix modelRotation;

        public Mob(Model mob, float x, float y, float z)
        {
            mobModel = mob;
            modelPosition = new Vector3(x, y, z);
            modelRotation = Matrix.CreateRotationX(MathHelper.Pi / 2);
        }

        public void Update(GameTime gameTime)
        {
            /*float time = gameTime.ElapsedGameTime.Milliseconds;
            float x = 0;
            float z = 0;
            if (modelPosition.X < 50)
            {
                x = 2 * time / 100;
                z = 0;
            }
            else
            {
                x = 0;
                z = 2 * time / 100;
                modelRotation = Matrix.CreateRotationY(2 * MathHelper.Pi);
            }
            modelPosition = new Vector3(modelPosition.X + x, modelPosition.Y, modelPosition.Z - z);*/
        }

        public void Draw()
        {
            Matrix[] transforms = new Matrix[mobModel.Bones.Count];
            mobModel.CopyAbsoluteBoneTransformsTo(transforms);

            //Matrix worldMatrix = Matrix.CreateScale(10f, 10f, 10f) * Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateTranslation(modelPosition);

            foreach (ModelMesh mesh in mobModel.Meshes)
            {
                //this is where the mesh orientation is set
                //as well as our camera and projection
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateScale(0.098f) * modelRotation * Matrix.CreateTranslation(modelPosition);
                    effect.View = Matrix.CreateLookAt(GlobalsVar.Camera.Position, GlobalsVar.Camera.LookAt, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), GlobalsVar.Camera.AspectRatio, 1.0f, 1000.0f);
                    //effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GlobalsVar.aspectRatio, 10, 10000);
                }

                mesh.Draw();
            }
        }
        
    }
}
