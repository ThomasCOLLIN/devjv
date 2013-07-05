using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ROTP.Scenes.Characters;
using Microsoft.Xna.Framework.Graphics;
using ROTP.Elements;

namespace ROTP.Utils
{
    class GlobalsVar
    {
        static Vector3 _cameraPosition;
        static float _aspectRatio;
        static Vector3 _cameraLookAt;
        static List<Mob> _mobs;
        static Dictionary<string, Model> meshModels;
        static Map map;

        public static Map Map
        {
            get { return GlobalsVar.map; }
            set { GlobalsVar.map = value; }
        }

        public static Dictionary<string, Model> MeshModels
        {
            get { return GlobalsVar.meshModels; }
            set { GlobalsVar.meshModels = value; }
        }

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
