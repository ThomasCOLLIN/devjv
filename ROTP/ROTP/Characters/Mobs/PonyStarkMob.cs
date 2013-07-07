using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROTP.Utils;
using Microsoft.Xna.Framework;

namespace ROTP.Characters.Mobs
{
    public class PonyStarkMob : Mob
    {   
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="position">Coordinates x, y, z of the mob in the scene.</param>
        public PonyStarkMob(Vector3 position) : base(position)
        {
            Type = "test";
            //model3D = GlobalsVar.MeshModels["mob1"];
            model3D = GlobalsVar.MeshModels["ponyStark"];
            modelRotation = Matrix.CreateRotationX(MathHelper.Pi / 2);
            ratio = 0.015f;
            modelVelocity = 0.05f;
            Life = 10;

            
        }
    }
}
