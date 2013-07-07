using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROTP.Characters;

namespace ROTP.Utils
{
    public class Comparers
    {
        /// <summary>
        /// Take two towers and compare there Y position.
        /// Can be use to display towers with the right order.
        /// </summary>
        /// <param name="tower1">1st tower.</param>
        /// <param name="tower2">2nd tower.</param>
        /// <returns></returns>
        public static Int32 TowerCompareY(Tower tower1, Tower tower2)
        {
            if (tower1.ModelPosition.Y < tower2.ModelPosition.Y)
                return 1;
            if (tower1.ModelPosition.Y > tower2.ModelPosition.Y)
                return -1;
            return 0;
        }
    }
}
