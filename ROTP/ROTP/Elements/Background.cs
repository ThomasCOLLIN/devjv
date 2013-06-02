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
        private Vector3 cameraPosition; // la position de la camera dans l'espace
        private Vector3 cameraLookAt;  // le point vers lequel la camera est dirigée

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
        }

        public void LoadContent(ContentManager content)
        {
            lightDirection.Normalize();
            effect = content.Load<Effect>("effects");
            sceneryTexture = content.Load<Texture2D>("Textures\\texturemap");

            SetUpCamera();
            LoadFloorPlan(20,15);
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
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                throw new UnauthorizedAccessException();

            UpdateCamera();
            UpdateMouseValues();
        }

        public void Draw()
        {
            _device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.DarkSlateBlue, 1.0f, 0);

            DrawCity();

            viewMatrix = Matrix.CreateLookAt(cameraPosition, cameraLookAt, Vector3.Up);
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
            cameraPosition = new Vector3(10, 2, -10);
            cameraLookAt = new Vector3(0, 2, 0);

        }

        /// <summary>
        /// Update the camera (position, direction...) using entries
        /// </summary>
        private void UpdateCamera()
        {
            // Listen to the  keyboard

            Vector3 dir = cameraLookAt - cameraPosition;
            dir.Normalize();
            dir /= 4; // Diminue la vitesse

            Vector3 dirN = dir;
            dirN.X = dir.Z;
            dirN.Z = -dir.X;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                cameraPosition += dir;
                cameraLookAt += dir;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                cameraPosition -= dir;
                cameraLookAt -= dir;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                cameraPosition += dirN;
                cameraLookAt += dirN;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                cameraPosition -= dirN;
                cameraLookAt -= dirN;
            }

            // Listen to the mouse
            if (Mouse.GetState().ScrollWheelValue > mouseScrollValue)
            {
                cameraPosition += dir * 10;
                cameraLookAt += dir * 10;
            }
            if (Mouse.GetState().ScrollWheelValue < mouseScrollValue)
            {
                cameraPosition -= dir * 2;
                cameraLookAt -= dir * 2;
            }

            
            if (((Mouse.GetState().LeftButton == ButtonState.Pressed) && Mouse.GetState().X > mouseX) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Matrix rotationMatrix = Matrix.CreateRotationY((float)0.01);
                Vector3 transformedReference = Vector3.Transform(cameraPosition - cameraLookAt, rotationMatrix);
                cameraPosition = cameraLookAt + transformedReference;
            }
            if (((Mouse.GetState().LeftButton == ButtonState.Pressed) && Mouse.GetState().X < mouseX) || Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(-(float)0.01);
                Vector3 transformedReference = Vector3.Transform(cameraPosition - cameraLookAt, rotationMatrix);
                cameraPosition = cameraLookAt + transformedReference;
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
            int cityWidth = floorPlan.GetLength(0);
            int cityLength = floorPlan.GetLength(1);
            int differentBuildings = buildingHeights.Length - 1;
            float imagesInTexture = 1 + differentBuildings * 2;

            List<VertexPositionNormalTexture> verticesList = new List<VertexPositionNormalTexture>();
            for (int x = 0; x < cityWidth; x++)
            {
                for (int z = 0; z < cityLength; z++)
                {
                    int currentbuilding = floorPlan[x, z];

                    //floor or ceiling
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, buildingHeights[currentbuilding], -z), new Vector3(0, 1, 0), new Vector2(currentbuilding * 2 / imagesInTexture, 1)));
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, buildingHeights[currentbuilding], -z - 1), new Vector3(0, 1, 0), new Vector2((currentbuilding * 2) / imagesInTexture, 0)));
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, buildingHeights[currentbuilding], -z), new Vector3(0, 1, 0), new Vector2((currentbuilding * 2 + 1) / imagesInTexture, 1)));

                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, buildingHeights[currentbuilding], -z - 1), new Vector3(0, 1, 0), new Vector2((currentbuilding * 2) / imagesInTexture, 0)));
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, buildingHeights[currentbuilding], -z - 1), new Vector3(0, 1, 0), new Vector2((currentbuilding * 2 + 1) / imagesInTexture, 0)));
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, buildingHeights[currentbuilding], -z), new Vector3(0, 1, 0), new Vector2((currentbuilding * 2 + 1) / imagesInTexture, 1)));

                    if (currentbuilding != 0)
                    {
                        //front wall
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -z - 1), new Vector3(0, 0, -1), new Vector2((currentbuilding * 2) / imagesInTexture, 1)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, buildingHeights[currentbuilding], -z - 1), new Vector3(0, 0, -1), new Vector2((currentbuilding * 2 - 1) / imagesInTexture, 0)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 0, -z - 1), new Vector3(0, 0, -1), new Vector2((currentbuilding * 2 - 1) / imagesInTexture, 1)));

                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, buildingHeights[currentbuilding], -z - 1), new Vector3(0, 0, -1), new Vector2((currentbuilding * 2 - 1) / imagesInTexture, 0)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -z - 1), new Vector3(0, 0, -1), new Vector2((currentbuilding * 2) / imagesInTexture, 1)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, buildingHeights[currentbuilding], -z - 1), new Vector3(0, 0, -1), new Vector2((currentbuilding * 2) / imagesInTexture, 0)));

                        //back wall
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -z), new Vector3(0, 0, 1), new Vector2((currentbuilding * 2) / imagesInTexture, 1)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 0, -z), new Vector3(0, 0, 1), new Vector2((currentbuilding * 2 - 1) / imagesInTexture, 1)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, buildingHeights[currentbuilding], -z), new Vector3(0, 0, 1), new Vector2((currentbuilding * 2 - 1) / imagesInTexture, 0)));

                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, buildingHeights[currentbuilding], -z), new Vector3(0, 0, 1), new Vector2((currentbuilding * 2 - 1) / imagesInTexture, 0)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, buildingHeights[currentbuilding], -z), new Vector3(0, 0, 1), new Vector2((currentbuilding * 2) / imagesInTexture, 0)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -z), new Vector3(0, 0, 1), new Vector2((currentbuilding * 2) / imagesInTexture, 1)));

                        //left wall
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 0, -z), new Vector3(-1, 0, 0), new Vector2((currentbuilding * 2) / imagesInTexture, 1)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 0, -z - 1), new Vector3(-1, 0, 0), new Vector2((currentbuilding * 2 - 1) / imagesInTexture, 1)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, buildingHeights[currentbuilding], -z - 1), new Vector3(-1, 0, 0), new Vector2((currentbuilding * 2 - 1) / imagesInTexture, 0)));

                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, buildingHeights[currentbuilding], -z - 1), new Vector3(-1, 0, 0), new Vector2((currentbuilding * 2 - 1) / imagesInTexture, 0)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, buildingHeights[currentbuilding], -z), new Vector3(-1, 0, 0), new Vector2((currentbuilding * 2) / imagesInTexture, 0)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 0, -z), new Vector3(-1, 0, 0), new Vector2((currentbuilding * 2) / imagesInTexture, 1)));

                        //right wall
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -z), new Vector3(1, 0, 0), new Vector2((currentbuilding * 2) / imagesInTexture, 1)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, buildingHeights[currentbuilding], -z - 1), new Vector3(1, 0, 0), new Vector2((currentbuilding * 2 - 1) / imagesInTexture, 0)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -z - 1), new Vector3(1, 0, 0), new Vector2((currentbuilding * 2 - 1) / imagesInTexture, 1)));

                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, buildingHeights[currentbuilding], -z - 1), new Vector3(1, 0, 0), new Vector2((currentbuilding * 2 - 1) / imagesInTexture, 0)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -z), new Vector3(1, 0, 0), new Vector2((currentbuilding * 2) / imagesInTexture, 1)));
                        verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, buildingHeights[currentbuilding], -z), new Vector3(1, 0, 0), new Vector2((currentbuilding * 2) / imagesInTexture, 0)));
                    }
                }
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
                        floorPlan[i, j] = 3;
                    else
                        floorPlan[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Draw the city
        /// </summary>
        private void DrawCity()
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
