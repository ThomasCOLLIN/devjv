using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ROTP.Utils;

namespace ROTP.Elements
{
    class Background
    {
        private GraphicsDevice _device;

        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private VertexBuffer cityVertexBuffer;

        private Effect effect;
        private Texture2D sceneryTexture;

        // Light and camera
        private Vector3 lightDirection = new Vector3(3, -2, 5);
        //private Vector3 cameraPosition; // la position de la camera dans l'espace
        //private Vector3 cameraLookAt;  // le point vers lequel la camera est dirigée

        // Mouse state
        private int mouseScrollValue;
        private int mouseX;
        private int mouseY;

        private int[,] floorPlan;
        private int[] buildingHeights = new int[] { 0, 2, 2, 6, 5, 4 };

        #region Public methods

        public Background(GraphicsDevice device)
        {
            _device = device;
            GlobalsVar.aspectRatio = device.Viewport.AspectRatio;
        }

        public void LoadContent(ContentManager content)
        {
            lightDirection.Normalize();
            effect = content.Load<Effect>("effects");
            sceneryTexture = content.Load<Texture2D>("Textures\\texturemap");

            SetUpCamera();
            LoadFloorPlan(50,60);
            SetUpVertices();
        }

        public void UnloadContent()
        {
        }

        public void Update()
        {
        }

        public void HandleInput()
        {
            UpdateCamera();
            UpdateMouseValues();
        }

        public void Draw()
        {
            _device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.DarkSlateBlue, 1.0f, 0);

            //DrawRoom();

            viewMatrix = Matrix.CreateLookAt(GlobalsVar.cameraPosition, GlobalsVar.cameraLookAt, Vector3.Up);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, _device.Viewport.AspectRatio, 0.2f, 500.0f);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Camera management
        /// </summary>
        private void SetUpCamera()
        {
            //Init
            //GlobalsVar.cameraPosition = new Vector3(20, 25, -30);
            //GlobalsVar.cameraLookAt = new Vector3(20, 0, 0);
            GlobalsVar.cameraPosition = new Vector3(0, 0, 40);
            GlobalsVar.cameraLookAt = new Vector3(0, 20, 0);

        }

        /// <summary>
        /// Update the camera (position, direction...) using entries
        /// </summary>
        private void UpdateCamera()
        {
            Vector3 dir = GlobalsVar.cameraLookAt - GlobalsVar.cameraPosition;
            dir.Z = 0;
            dir.Normalize();
            dir /= 4; // Diminue la vitesse

            Vector3 dirN = dir;
            dirN.X = -dir.Y;
            dirN.Y = dir.X;

            // Gauche - Droite
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                /*if (GlobalsVar.cameraPosition.X + dirN.X < floorPlan.GetLength(0) - 1 && GlobalsVar.cameraPosition.X + dirN.X > 1
                    && -(GlobalsVar.cameraPosition.Z + dirN.Z) < floorPlan.GetLength(1) -1 && -(GlobalsVar.cameraPosition.Z + dirN.Z) > 1)
                {*/
                    GlobalsVar.cameraPosition += dirN;
                    GlobalsVar.cameraLookAt += dirN;
                //}
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                /*if (GlobalsVar.cameraPosition.X - dirN.X < floorPlan.GetLength(0) - 1 && GlobalsVar.cameraPosition.X - dirN.X > 1
                    && -(GlobalsVar.cameraPosition.Z - dirN.Z) < floorPlan.GetLength(1) - 1 && -(GlobalsVar.cameraPosition.Z - dirN.Z) > 1)
                {*/
                    GlobalsVar.cameraPosition -= dirN;
                    GlobalsVar.cameraLookAt -= dirN;
                //}
            }

            // Travelling
            if (Mouse.GetState().ScrollWheelValue > mouseScrollValue)
            {
                if (GlobalsVar.cameraPosition.X + (dir.X * 10) < floorPlan.GetLength(0) - 1 && GlobalsVar.cameraPosition.X + (dir.X * 10) > 1
                    && -(GlobalsVar.cameraPosition.Z + (dir.Z * 10)) < floorPlan.GetLength(1) - 1 && -(GlobalsVar.cameraPosition.Z + (dir.Z * 10)) > 1)
                {
                    GlobalsVar.cameraPosition += dir * 10;
                    GlobalsVar.cameraLookAt += dir * 10;
                }
            }
            if (Mouse.GetState().ScrollWheelValue < mouseScrollValue)
            {
                if (GlobalsVar.cameraPosition.X - (dir.X * 10) < floorPlan.GetLength(0) - 1 && GlobalsVar.cameraPosition.X - (dir.X * 10) > 1
                    && -(GlobalsVar.cameraPosition.Z - (dir.Z * 10)) < floorPlan.GetLength(1) - 1 && -(GlobalsVar.cameraPosition.Z - (dir.Z * 10)) > 1)
                {
                    GlobalsVar.cameraPosition -= dir * 10;
                    GlobalsVar.cameraLookAt -= dir * 10;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                /*if (GlobalsVar.cameraPosition.X + dir.X < floorPlan.GetLength(0) - 1 && GlobalsVar.cameraPosition.X + dir.X > 1
                    && -(GlobalsVar.cameraPosition.Z + dir.Z) < floorPlan.GetLength(1) - 1 && -(GlobalsVar.cameraPosition.Z + dir.Z) > 1)
                {*/
                    GlobalsVar.cameraPosition += dir;
                    GlobalsVar.cameraLookAt += dir;
                //}
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                /*if (GlobalsVar.cameraPosition.X - dir.X < floorPlan.GetLength(0) - 1 && GlobalsVar.cameraPosition.X - dir.X > 1
                    && -(GlobalsVar.cameraPosition.Z - dir.Z) < floorPlan.GetLength(1) - 1 && -(GlobalsVar.cameraPosition.Z - dir.Z) > 1)
                {*/
                    GlobalsVar.cameraPosition -= dir;
                    GlobalsVar.cameraLookAt -= dir;
                //}
            }
            
            // rotation autour d'un point
            float angle = (float) 0.01;
            if ((Mouse.GetState().RightButton == ButtonState.Pressed) && Mouse.GetState().X > mouseX)
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(angle);
                Vector3 transformedReference = Vector3.Transform(GlobalsVar.cameraPosition - GlobalsVar.cameraLookAt, rotationMatrix);

                if ((GlobalsVar.cameraLookAt + transformedReference).X < floorPlan.GetLength(0) - 1 && (GlobalsVar.cameraLookAt + transformedReference).X > 1
                    && -(GlobalsVar.cameraLookAt + transformedReference).Z < floorPlan.GetLength(1) - 1 && -(GlobalsVar.cameraLookAt + transformedReference).Z > 1)
                {
                    GlobalsVar.cameraPosition = GlobalsVar.cameraLookAt + transformedReference;
                }
            }
            if ((Mouse.GetState().RightButton == ButtonState.Pressed) && Mouse.GetState().X < mouseX)
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(-angle);
                Vector3 transformedReference = Vector3.Transform(GlobalsVar.cameraPosition - GlobalsVar.cameraLookAt, rotationMatrix);

                if ((GlobalsVar.cameraLookAt + transformedReference).X < floorPlan.GetLength(0) - 1 && (GlobalsVar.cameraLookAt + transformedReference).X > 1
                    && -(GlobalsVar.cameraLookAt + transformedReference).Z < floorPlan.GetLength(1) - 1 && -(GlobalsVar.cameraLookAt + transformedReference).Z > 1)
                {
                    GlobalsVar.cameraPosition = GlobalsVar.cameraLookAt + transformedReference;
                }
            }
            if ((Mouse.GetState().RightButton == ButtonState.Pressed) && Mouse.GetState().Y > mouseY)
            {
                Matrix rotationX = Matrix.CreateRotationX(-angle * dirN.X * 4);
                Matrix rotationZ = Matrix.CreateRotationZ(-angle * dirN.Z * 4);
                Matrix rotationMatrix = rotationX * rotationZ;
                Vector3 transformedReference = Vector3.Transform(GlobalsVar.cameraPosition - GlobalsVar.cameraLookAt, rotationMatrix);

                if ((GlobalsVar.cameraLookAt + transformedReference).X < floorPlan.GetLength(0) - 1 && (GlobalsVar.cameraLookAt + transformedReference).X > 1
                    && -(GlobalsVar.cameraLookAt + transformedReference).Z < floorPlan.GetLength(1) - 1 && -(GlobalsVar.cameraLookAt + transformedReference).Z > 1
                    && (GlobalsVar.cameraLookAt + transformedReference).Y > 0.05 && (GlobalsVar.cameraLookAt + transformedReference).Y < 21)
                {
                    GlobalsVar.cameraPosition = GlobalsVar.cameraLookAt + transformedReference;
                }
            }
            if ((Mouse.GetState().RightButton == ButtonState.Pressed) && Mouse.GetState().Y < mouseY)
            {
                Matrix rotationX = Matrix.CreateRotationX(angle * dirN.X * 4);
                Matrix rotationZ = Matrix.CreateRotationZ(angle * dirN.Z * 4);
                Matrix rotationMatrix = rotationX * rotationZ;
                Vector3 transformedReference = Vector3.Transform(GlobalsVar.cameraPosition - GlobalsVar.cameraLookAt, rotationMatrix);

                if ((GlobalsVar.cameraLookAt + transformedReference).X < floorPlan.GetLength(0) - 1 && (GlobalsVar.cameraLookAt + transformedReference).X > 1
                    && -(GlobalsVar.cameraLookAt + transformedReference).Z < floorPlan.GetLength(1) - 1 && -(GlobalsVar.cameraLookAt + transformedReference).Z > 1
                    && (GlobalsVar.cameraLookAt + transformedReference).Y > 0.05 && (GlobalsVar.cameraLookAt + transformedReference).Y < 21)
                {
                    GlobalsVar.cameraPosition = GlobalsVar.cameraLookAt + transformedReference;
                }
            }

            // rotation de la caméra
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Matrix rotationMatrix = Matrix.CreateRotationY((float)0.01);
                Vector3 transformedReference = Vector3.Transform(GlobalsVar.cameraLookAt - GlobalsVar.cameraPosition, rotationMatrix);
                GlobalsVar.cameraLookAt = GlobalsVar.cameraPosition + transformedReference;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(-(float)0.01);
                Vector3 transformedReference = Vector3.Transform(GlobalsVar.cameraLookAt - GlobalsVar.cameraPosition, rotationMatrix);
                GlobalsVar.cameraLookAt = GlobalsVar.cameraPosition + transformedReference;
            }
        }

        private void UpdateMouseValues()
        {
            mouseScrollValue = Mouse.GetState().ScrollWheelValue;
            mouseX = Mouse.GetState().X;
            mouseY = Mouse.GetState().Y;
        }

        /// <summary>
        /// Vertices
        /// </summary>
        private void SetUpVertices()
        {
            int width = floorPlan.GetLength(0);
            int length = floorPlan.GetLength(1);
            int differentBuildings = buildingHeights.Length - 1;
            float imagesInTexture = 1 + differentBuildings * 2;
            int texture;

            List<VertexPositionNormalTexture> verticesList = new List<VertexPositionNormalTexture>();
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < length; z++)
                {
                    texture = floorPlan[x, z];
                    //floor or ceiling
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 0, -z), new Vector3(0, 1, 0), new Vector2(texture / imagesInTexture, 1)));
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 0, -z - 1), new Vector3(0, 1, 0), new Vector2(texture / imagesInTexture, 0)));
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -z), new Vector3(0, 1, 0), new Vector2((texture  + 1) / imagesInTexture, 1)));

                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 0, -z - 1), new Vector3(0, 1, 0), new Vector2(texture  / imagesInTexture, 0)));
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -z - 1), new Vector3(0, 1, 0), new Vector2((texture + 1) / imagesInTexture, 0)));
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -z), new Vector3(0, 1, 0), new Vector2((texture + 1) / imagesInTexture, 1)));
                }

                // Wall 1
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, 0), new Vector3(0, 0, -1), new Vector2((3 * 2) / imagesInTexture, 1)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 10, 0), new Vector3(0, 0, -1), new Vector2((3 * 2 - 1) / imagesInTexture, 0)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 0, 0), new Vector3(0, 0, -1), new Vector2((3 * 2 - 1) / imagesInTexture, 1)));
                
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 10, 0), new Vector3(0, 0, -1), new Vector2((3 * 2 - 1) / imagesInTexture, 0)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, 0), new Vector3(0, 0, -1), new Vector2((3 * 2) / imagesInTexture, 1)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 10, 0), new Vector3(0, 0, -1), new Vector2((3 * 2) / imagesInTexture, 0)));

                // Wall 3
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -length), new Vector3(0, 0, 1), new Vector2((3 * 2) / imagesInTexture, 1)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 0, -length), new Vector3(0, 0, 1), new Vector2((3 * 2 - 1) / imagesInTexture, 1)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 10, -length), new Vector3(0, 0, 1), new Vector2((3 * 2 - 1) / imagesInTexture, 0)));

                verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 10, -length), new Vector3(0, 0, 1), new Vector2((3 * 2 - 1) / imagesInTexture, 0)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 10, -length), new Vector3(0, 0, 1), new Vector2((3 * 2) / imagesInTexture, 0)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -length), new Vector3(0, 0, 1), new Vector2((3 * 2) / imagesInTexture, 1)));
            }

            for (int z = 0; z < length; z++)
            {
                // Wall 2
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(width, 0, -z), new Vector3(-1, 0, 0), new Vector2((3 * 2) / imagesInTexture, 1)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(width, 0, -z - 1), new Vector3(-1, 0, 0), new Vector2((3 * 2 - 1) / imagesInTexture, 1)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(width, 10, -z - 1), new Vector3(-1, 0, 0), new Vector2((3 * 2 - 1) / imagesInTexture, 0)));

                verticesList.Add(new VertexPositionNormalTexture(new Vector3(width, 10, -z - 1), new Vector3(-1, 0, 0), new Vector2((3 * 2 - 1) / imagesInTexture, 0)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(width, 10, -z), new Vector3(-1, 0, 0), new Vector2((3 * 2) / imagesInTexture, 0)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(width, 0, -z), new Vector3(-1, 0, 0), new Vector2((3 * 2) / imagesInTexture, 1)));

                // Wall 4
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(0, 0, -z), new Vector3(1, 0, 0), new Vector2((3 * 2) / imagesInTexture, 1)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(0, 10, -z - 1), new Vector3(1, 0, 0), new Vector2((3 * 2 - 1) / imagesInTexture, 0)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(0, 0, -z - 1), new Vector3(1, 0, 0), new Vector2((3 * 2 - 1) / imagesInTexture, 1)));

                verticesList.Add(new VertexPositionNormalTexture(new Vector3(0, 10, -z - 1), new Vector3(1, 0, 0), new Vector2((3 * 2 - 1) / imagesInTexture, 0)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(0, 0, -z), new Vector3(1, 0, 0), new Vector2((3 * 2) / imagesInTexture, 1)));
                verticesList.Add(new VertexPositionNormalTexture(new Vector3(0, 10, -z), new Vector3(1, 0, 0), new Vector2((3 * 2) / imagesInTexture, 0)));

            }

            cityVertexBuffer = new VertexBuffer(_device, VertexPositionNormalTexture.VertexDeclaration, verticesList.Count, BufferUsage.WriteOnly);
            cityVertexBuffer.SetData<VertexPositionNormalTexture>(verticesList.ToArray());        
            
        }

        /// <summary>
        /// Create the map
        /// </summary>
        /// <param name="x">Size of the map</param>
        /// <param name="y">Size of the map</param>
        private void LoadFloorPlan(int x, int y)
        {
            floorPlan = new int[x,y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if ((i == 0) || (i == x - 1) || (j == 0) || (j == y - 1))
                        floorPlan[i, j] = 1;
                    else
                        floorPlan[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Draw the room
        /// </summary>
        private void DrawRoom()
        {
            effect.CurrentTechnique = effect.Techniques["Textured"];
            effect.Parameters["xWorld"].SetValue(Matrix.Identity);
            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
            effect.Parameters["xTexture"].SetValue(sceneryTexture);
            effect.Parameters["xEnableLighting"].SetValue(true);
            effect.Parameters["xLightDirection"].SetValue(lightDirection);
            effect.Parameters["xAmbient"].SetValue(0.5f);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _device.SetVertexBuffer(cityVertexBuffer);
                _device.DrawPrimitives(PrimitiveType.TriangleList, 0, cityVertexBuffer.VertexCount / 3);
            }


        }

        #endregion
    }
}
