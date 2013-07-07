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
    /// Earth tower.
    /// </summary>
    public class EarthTower : Tower
    {
        /// <summary>
        /// Constructor.s
        /// </summary>
        /// <param name="position">Coordinates x, y, z of the tower in the scene.</param>
        public EarthTower(Vector3 position) : base(position)
        {
            ActionRange = 3;
            Type = "earth";
            model3D = GlobalsVar.MeshModels["earthTower"];
            modelRotation = Matrix.CreateRotationX(MathHelper.Pi / 2);
            ratio = 0.02f;
        }
    }
}
