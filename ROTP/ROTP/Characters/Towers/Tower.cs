using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ROTP.Utils;

namespace ROTP.Characters
{
    /// <summary>
    /// Mother class of all the towers.
    /// </summary>
    public abstract class Tower
    {
        public float ActionRange { get; set; }
        public Vector3 ModelPosition { get; set; }
        public String Type { get; set; }
        
        protected Model model3D;
        protected Matrix modelRotation;
        protected float ratio;
        protected float reach;
        protected TimeSpan speedAttack;
        protected TimeSpan timeLastShot;

        public Tower(Vector3 position)
        {
            ModelPosition = position;
            modelRotation = Matrix.Identity;
            reach = 7.5f;
            speedAttack = new TimeSpan(0, 0, 2);
            timeLastShot = new TimeSpan(speedAttack.Hours, speedAttack.Minutes, speedAttack.Seconds);
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

        public void Update(GameTime gameTime)
        {
            timeLastShot = timeLastShot.Add(gameTime.ElapsedGameTime);

            if (speedAttack <= timeLastShot && GlobalsVar.Mobs.Any())
            {
                Console.WriteLine("try to attack");
                // if the tower has attacked this time, the timer is reset.
                if (Attack(GlobalsVar.Mobs))
                    timeLastShot = new TimeSpan(0, 0, 0);
            }
        }

        public Boolean Attack(List<Mob> mobs)
        {
            foreach (Mob mob in mobs)
            {
                Boolean isCollide = TestCollisions.Intersects(new Vector2(ModelPosition.X, ModelPosition.Y), reach,
                    new Vector2(mob.ModelPosition.X, mob.ModelPosition.Y), new Vector2(5, 5));
                if (isCollide)
                {
                    mob.LostLife();
                    return true;
                }
            }
            return false;
        }

        public void HandleInput()
        {

        }
    }
}
