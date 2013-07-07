using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ROTP.Utils;

namespace ROTP.Characters
{
    public abstract class Mob
    {
        public Vector3 ModelPosition { get; set; }
        public String Type { get; set; }
        public Int32 Life { get; set; }
        //public Re MyProperty { get; set; }
        
        protected Model model3D;
        protected float modelVelocity;
        protected Matrix modelRotation;
        protected float ratio;
        protected Int32 damages;

        public Mob(Vector3 position)
        {
            ModelPosition = position;
            modelRotation = Matrix.Identity;
            damages = 1;
        }

        public void Update(GameTime gameTime)
        {
            ModelPosition = new Vector3(ModelPosition.X + modelVelocity, ModelPosition.Y, ModelPosition.Z);

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

        public void LostLife()
        {
            Life -= damages;
            Console.WriteLine("-" + damages + ". Life is now: " + Life);
            if (Life <= 0)
            {
                GlobalsVar.Mobs.Remove(this);
                Console.WriteLine("Arrg!");
            }
        }

        public void Draw(GameTime gameTime)
        {
            // Copy any parent transforms.
            Matrix[] transforms = new Matrix[model3D.Bones.Count];
            model3D.CopyAbsoluteBoneTransformsTo(transforms);

            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in model3D.Meshes)
            {
                // This is where the mesh orientation is set, as well 
                // as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index]
                        * Matrix.CreateScale(ratio) * modelRotation
                        * Matrix.CreateTranslation(ModelPosition);

                    effect.View = Matrix.CreateLookAt(GlobalsVar.Camera.Position,
                        GlobalsVar.Camera.LookAt, Vector3.Up);

                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        MathHelper.ToRadians(45.0f), GlobalsVar.Camera.AspectRatio,
                        1.0f, 10000.0f);
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }
        }
        
    }
}
