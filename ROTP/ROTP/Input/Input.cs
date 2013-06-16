using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ROTP.Input
{
    class Input : GameComponent
    {
        private static KeyboardState _keyboardState;
        private static KeyboardState _previousKeyboardState;

        private static GamePadState _gamePadState;
        private static GamePadState _previousGamePadState;

        public static KeyboardState KeyboardState
        {
            get { return _keyboardState; }
        }

        public static GamePadState GamePadState

        {
            get { return _gamePadState; }
        }

        public Input(Game game)
            : base(game)
        {
            _keyboardState = new KeyboardState();
            _previousKeyboardState = new KeyboardState();

            _gamePadState = new GamePadState();
            _previousGamePadState = new GamePadState();
        }

        public override void  Update(GameTime gameTime)
        {
         	 base.Update(gameTime);

            _previousKeyboardState = _keyboardState;
            _previousGamePadState = _gamePadState;

            _keyboardState = Keyboard.GetState();
            _gamePadState = GamePad.GetState(PlayerIndex.One);
        }

        protected static bool IsKeyPressedOnce(Keys key)
        {
            return _previousKeyboardState.IsKeyUp(key)
                && _keyboardState.IsKeyDown(key);
        }

        protected static bool IsButtonPressedOnce(Buttons button)
        {
            return _previousGamePadState.IsButtonUp(button)
                && _gamePadState.IsButtonDown(button);
        }
    }
}
