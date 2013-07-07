using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ROTP.Elements;
using ROTP.Characters;

namespace ROTP.Utils
{
    public class GlobalsVar
    {
        public static Dictionary<string, Model> MeshModels { get; set; }
        public static Map Map { get; set; }
        public static List<Mob> Mobs { get; set; }
        internal static Camera Camera { get; set; }
        public static Int32 PlayerLife { get; set; }
    }
}
