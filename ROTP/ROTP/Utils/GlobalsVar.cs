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
        static List<Mob> _mobs;
        static Dictionary<string, Model> meshModels;
        static Map map;
        static Camera camera;

        internal static Camera Camera
        {
            get { return GlobalsVar.camera; }
            set { GlobalsVar.camera = value; }
        }

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
