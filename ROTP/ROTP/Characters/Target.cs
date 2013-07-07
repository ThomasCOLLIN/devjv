using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ROTP.Utils;

namespace ROTP.Characters
{
    public class Target
    {
        public Vector3 ModelPosition { get; set; }
        
        protected Model model3D;
        protected Matrix modelRotation;
        protected float ratio;
        protected float reach;

        public Target(Vector3 position)
        {
            ModelPosition = position;
            modelRotation = Matrix.Identity;

            model3D = GlobalsVar.MeshModels["hq"];
            modelRotation = Matrix.CreateRotationX(MathHelper.Pi / 2);
            ratio = 0.08f;
            reach = 5f;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < GlobalsVar.Mobs.Count; i++)
            {
                Mob mob = GlobalsVar.Mobs.ElementAt(i);
                Boolean isCollide = TestCollisions.Intersects(new Vector2(ModelPosition.X, ModelPosition.Y), reach,
                        new Vector2(mob.ModelPosition.X, mob.ModelPosition.Y), new Vector2(5, 5));
                if (isCollide)
                {
                    mob.Kill();
                    GlobalsVar.PlayerLife -= 1;
                    Console.WriteLine("The player have now: " + GlobalsVar.PlayerLife);
                }
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
