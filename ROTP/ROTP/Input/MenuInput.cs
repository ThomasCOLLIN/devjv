using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ROTP.Input
{
    class MenuInput : Input
    {
        public MenuInput(Game game)
            : base(game)
        {
        }

        public static bool IsDownPressed()
        {
            return IsKeyPressedOnce(Keys.Down)
                || IsButtonPressedOnce(Buttons.DPadDown)
                || IsButtonPressedOnce(Buttons.LeftThumbstickDown);
        }

        public static bool IsUpPressed()
        {
            return IsKeyPressedOnce(Keys.Up)
                || IsButtonPressedOnce(Buttons.DPadUp)
                || IsButtonPressedOnce(Buttons.LeftThumbstickUp);
        }

        public static bool IsSelectPressed()
        {
            return IsKeyPressedOnce(Keys.Enter)
                || IsButtonPressedOnce(Buttons.A);
        }

        public static bool IsCancelPressed()
        {
            return IsKeyPressedOnce(Keys.Escape)
                || IsButtonPressedOnce(Buttons.B);
        }
    }
}
