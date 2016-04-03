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

namespace gridgame
{
    public class InputHandle
    {
        KeyboardState cur;
        KeyboardState past;
        public InputHandle()
        {
            cur = Keyboard.GetState();
        }

        public void Update()
        {
            past = cur;
            cur = Keyboard.GetState();
        }

        public bool wasKeyPressed(Keys K)
        {
            if (cur.IsKeyDown(K) && !past.IsKeyDown(K))
            { return true; }
            else { return false; }
        }
    }
}
