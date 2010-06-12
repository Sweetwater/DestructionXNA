using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace DestructionXNA.Utility
{
    public class InputState
    {
        public KeyboardState NewKeyboardState { get; private set;}
        public KeyboardState OldKeyboardState { get; private set; }

        public MouseState NewMouseState { get; private set; }
        public MouseState OldMouseState { get; private set; }

        public InputState() {
        }

        public void Update() {
            OldKeyboardState = NewKeyboardState;
            OldMouseState = NewMouseState;

            NewKeyboardState = Keyboard.GetState();
            NewMouseState = Mouse.GetState();
        }

        public bool IsDown(Keys key) {
            return NewKeyboardState.IsKeyDown(key);
        }
        public bool IsUp(Keys key)
        {
            return NewKeyboardState.IsKeyUp(key);
        }
        public bool IsTrigger(Keys key)
        {
            return IsDown(key) && OldKeyboardState.IsKeyUp(key);
        }
        public bool IsRelase(Keys key)
        {
            return IsUp(key) && OldKeyboardState.IsKeyDown(key);
        }
    }
}
