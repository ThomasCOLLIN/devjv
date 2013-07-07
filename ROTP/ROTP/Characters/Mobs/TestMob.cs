using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROTP.Utils;
using Microsoft.Xna.Framework;

namespace ROTP.Characters.Mobs
{
    public class TestMob : Mob
    {   
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="position">Coordinates x, y, z of the mob in the scene.</param>
        public TestMob(Vector3 position) : base(position)
        {
            Type = "test";
            model3D = GlobalsVar.MeshModels["mob1"];
            modelRotation = Matrix.CreateRotationX(MathHelper.Pi / 2);
            ratio = 0.05f;
            modelVelocity = 0.03f;
            Life = 10;

            
        }
    }
}
