using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ROTP.Utils;

namespace ROTP.Characters
{
    public class WaterTower : Tower
    {
        /// <summary>
        /// Constructor.s
        /// </summary>
        /// <param name="position">Coordinates x, y, z of the tower in the scene.</param>
        public WaterTower(Vector3 position) : base(position)
        {
            ActionRange = 3;
            Type = "fire";
            model3D = GlobalsVar.MeshModels["waterTower"];
            modelRotation = Matrix.CreateRotationY(3*MathHelper.Pi / 2);
            ratio = 0.05f;
        }
    }
}
