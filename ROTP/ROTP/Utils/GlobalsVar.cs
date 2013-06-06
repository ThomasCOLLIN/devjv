using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ROTP.Utils
{
    class GlobalsVar
    {
        static Vector3 _cameraPosition;
        static float _aspectRatio;
        static Vector3 _cameraLookAt;

        public static Vector3 cameraPosition
        {
            get
            {
                return _cameraPosition;
            }
            set
            {
                _cameraPosition = value;
            }
        }

        public static float aspectRatio
        {
            get
            {
                return _aspectRatio;
            }
            set
            {
                _aspectRatio = value;
            }
        }

        public static Vector3 cameraLookAt
        {
            get
            {
                return _cameraLookAt;
            }
            set
            {
                _cameraLookAt = value;
            }
        }
    }
}
