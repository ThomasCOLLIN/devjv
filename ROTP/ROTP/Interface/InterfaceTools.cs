using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ROTP.Interface
{
    public static class InterfaceTools
    {
        public static Boolean isMouseLeftPressed(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }

        public static Boolean isMouseIntersects(MouseState mouseState, Vector2 texturePosition, Texture2D texture)
        {
            Rectangle mouseRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
            Rectangle objectRect = new Rectangle((Int32)texturePosition.X, (Int32)texturePosition.Y, texture.Width, texture.Height);

            return objectRect.Intersects(mouseRect);
        }
    }
}
