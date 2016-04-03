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
    public class Rechteck
    {
        public static Texture2D Recht; 
        public int Xpos = 0;
        public int Ypos = 0;

        public Rechteck(int Xpos, int Ypos)
        {
            this.Xpos = Xpos;
            this.Ypos = Ypos;
        }   


        public static void Load(GraphicsDevice GD)
        {
            Recht = new Texture2D(GD, 1, 1);
            Color[] Col = new Color[1];
            Col[0] = Color.White;
            Recht.SetData(Col);
            
        
        }
    }
}
