using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ROTP.Interface
{
    public static class ToolsInterface
    {
        public static Boolean isMouseLeftPressed()
        {
            MouseState mouseState = Mouse.GetState();
            return (mouseState.LeftButton == ButtonState.Pressed);
        }

        public static Boolean isMouseIntersects(Vector2 objectPosition, Rectangle objectBoundsRect)
        {
            MouseState mouseState = Mouse.GetState();

            Rectangle mouseRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
            
            return objectBoundsRect.Intersects(mouseRect);
        }
    }
}
