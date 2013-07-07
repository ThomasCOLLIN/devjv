using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ROTP.Characters.Towers
{
    public class TowerFactory
    {
        /// <summary>
        /// Generate a tower "type" at the position "position".
        /// </summary>
        /// <param name="type">Type of the tower.</param>
        /// <param name="position">Position of the tower on the map.</param>
        /// <returns></returns>
        public static Tower GenerateTower(String type, Vector3 position)
        {
            switch (type)
            {   
                case "earth":
                    return new EarthTower(position);
                case "water":
                    return new WaterTower(position);
                default:
                    return new FireTower(position);
            }
        }
    }
}
