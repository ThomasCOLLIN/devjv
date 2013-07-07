using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ROTP.Utils;

namespace ROTP.Elements
{
    class MapCase
    {
        private Model mapModel;
        private Vector3 modelPosition;
        private Matrix modelRotation;
        private float sizeX;
        private float sizeZ;
        private float ratio;
        private string type;

        #region getters

        public Vector3 ModelPosition
        {
            get { return modelPosition; }
            set { modelPosition = value; }
        }
        public Model MapModel
        {
            get { return mapModel; }
            set { mapModel = value; }
        }
        public float SizeX
        {
          get { return sizeX; }
          set { sizeX = value; }
        }

        public float SizeZ
        {
          get { return sizeZ; }
          set { sizeZ = value; }
        }

        public float Ratio
        {
          get { return ratio; }
          set { ratio = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        #endregion

        public MapCase(Model map, float x, float y, float z, float sizex, float sizez, float ratio)
        {
            mapModel = map;
            modelPosition = new Vector3(x, y, z);
            modelRotation = Matrix.CreateRotationX(MathHelper.Pi / 2);
            this.sizeX = sizex;
            this.sizeZ = sizez;
            this.ratio = ratio;//ratio par défaut pour le model de map que j'ai créé, 0.1
            this.type = "grass";
        }

        public void Update(GameTime gameTime)
        {
            //Normalement rien à changer sur le sol mais on sait jamais
        }

        public void Draw()
        {
            Matrix[] transforms = new Matrix[mapModel.Bones.Count];
            mapModel.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in mapModel.Meshes)
            {
                //this is where the mesh orientation is set
                //as well as our camera and projection
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] 
                        * Matrix.CreateScale(ratio) * modelRotation 
                        * Matrix.CreateTranslation(modelPosition);

                    effect.View = Matrix.CreateLookAt(GlobalsVar.Camera.Position, 
                        GlobalsVar.Camera.LookAt, Vector3.Up);

                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        MathHelper.ToRadians(45.0f), GlobalsVar.Camera.AspectRatio, 
                        1.0f, 1000.0f);
                }

                mesh.Draw();
            }
        } 
    }
}
