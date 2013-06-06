using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ROTP.Scenes.Characters;

namespace ROTP.Utils
{
    class GlobalsVar
    {
        static Vector3 _cameraPosition;
        static float _aspectRatio;
        static Vector3 _cameraLookAt;
        static List<Mob> _mobs;

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

        public static List<Mob> mobs
        {
            get
            {
                return _mobs;
            }
            set
            {
                _mobs = value;
            }
        }
    }
}
