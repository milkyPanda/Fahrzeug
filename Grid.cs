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
    class Grid     //defaults to an internal class
    {
        private Texture2D gridtex;
        
        int width;      //private by default
        int height;
        int pixwidth;
        int pixheight;
        int fieldwidth;
        
        public Grid(GraphicsDevice dev, int w, int h, int fieldwidth)
        {
            this.width = w;
            this.height = h;
            this.pixwidth = width*fieldwidth;
            this.pixheight = h*fieldwidth;
            this.fieldwidth = fieldwidth;

            gridtex = new Texture2D(dev, pixwidth, pixheight );
            
            //Color array for the grid texture
            Color[] col = new Color[pixwidth*pixheight];
            //set whole grid to black
            for(int i = 0; i < pixwidth*pixheight; i++)
            {
                col[i] = Color.Black;
            }
            
            gridtex.SetData(col);

        }
        
        public Texture2D get_grid()
        {
            return gridtex;
        }
        
        public int pixWidth()
        {
            return pixwidth;
        }
        
        public int pixHeight()
        {
            return pixheight;
        }
    }
}
