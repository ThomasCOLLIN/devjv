using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ROTP.Utils
{
    public class TestCollisions
    {
        /// <summary>
        /// Detect the collision between a circle and a rectangle.
        /// </summary>
        /// <param name="circlePos">Position x, y of the center of the circle.</param>
        /// <param name="circleRayon">Rayon of the circle.</param>
        /// <param name="rectPos">Position y, y of the center of the rectangle.</param>
        /// <param name="rectSize">Size (width, height) of the rectangle.</param>
        /// <returns></returns>
        public static bool Intersects(Vector2 circlePos, float circleRayon, Vector2 rectPos, Vector2 rectSize)
        {
            float circleDistanceX = Math.Abs(circlePos.X - rectPos.X);
            float circleDistanceY = Math.Abs(circlePos.Y - rectPos.Y);

            if (circleDistanceX > (rectSize.X / 2 + circleRayon)) { return false; }
            if (circleDistanceY > (rectSize.Y / 2 + circleRayon)) { return false; }

            if (circleDistanceX <= (rectSize.X / 2)) { return true; } 
            if (circleDistanceY <= (rectSize.Y / 2)) { return true; }

            float cornerDistanceSq = (circleDistanceX - (float)Math.Pow((Double)rectSize.X / 2, 2) 
                + circleDistanceY - (float)Math.Pow((Double)rectSize.Y / 2, 2));

            return (cornerDistanceSq <= (float)Math.Pow(circleRayon, 2));
        }
    }
}
