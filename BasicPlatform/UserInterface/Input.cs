using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Input;

namespace BasicPlatform.Desktop
{
    public class Input
    {
        public KeyboardState kb, okb;
        public bool shift_down, control_down, alt_down;
        public bool shift_press, control_press, alt_press;
        public bool old_shift_down, old_control_down, old_alt_down;

        public Input()
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Keypress(Keys k)
        {
            return kb.IsKeyDown(k) && okb.IsKeyUp(k);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Keydown(Keys k)
        {
            return kb.IsKeyDown(k);
        }

        public void Update()
        {
            old_alt_down = alt_down;
            old_shift_down = shift_down;
            old_control_down = control_down;
            okb = kb;
            kb = Keyboard.GetState();
            shift_down = kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift);
            control_down = kb.IsKeyDown(Keys.LeftControl) || kb.IsKeyDown(Keys.RightControl);
            alt_down = kb.IsKeyDown(Keys.LeftAlt) || kb.IsKeyDown(Keys.RightAlt);
            shift_press = shift_down && !old_shift_down;
            alt_press = alt_down && !old_alt_down;
            control_press = control_press && !old_control_down;
        }
    }
}
