using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROTP.Utils;
using Microsoft.Xna.Framework;

namespace ROTP.Elements
{
    class Map
    {
        List<List<MapCase>> mapArray;

        internal List<List<MapCase>> MapArray
        {
            get { return mapArray; }
            set { mapArray = value; }
        }

        public Map()
        {
            mapArray = new List<List<MapCase>>();
        }

        public void Generate(int x, int y, string type)
        {
            for (int i = 0; i < x; i++)
            {
                mapArray.Add(new List<MapCase>());
                for (int j = 0; j < y; j++)
                {
                    MapCase newCase = null;
                    switch (type)
                    {
                        case "grass":
                            newCase = new MapCase(GlobalsVar.MeshModels["grassGround"], i * 5, j * 5, 0, 5, 5, 0.098f);
                            break;
                        case "dalles":
                            break;
                        default:
                            newCase = new MapCase(GlobalsVar.MeshModels["grassGround"], i * 5, j * 5, 0, 5, 5, 0.098f);
                            break;
                    }   
                    mapArray[i].Add(newCase);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < mapArray.Count; i++)
            {
                for (int j = 0; j < mapArray[i].Count; j++)
                {
                    mapArray[i][j].Update(gameTime);
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < mapArray.Count; i++)
            {
                for (int j = 0; j < mapArray[i].Count; j++)
                {
                    mapArray[i][j].Draw();
                }
            }
        }
    }
}
