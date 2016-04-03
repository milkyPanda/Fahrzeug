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
        int Xpos;
        int Ypos;
        int gridwidth;
        int gridheight;

        public Rechteck(int Xpos, int Ypos, int gridwidth, int gridheight)
        {
            this.Xpos = Xpos;
            this.Ypos = Ypos;
            this.gridwidth = gridwidth;
            this.gridheight = gridheight;
        }   


        public void Load(GraphicsDevice GD)
        {
            Recht = new Texture2D(GD, 1, 1);
            Color[] Col = new Color[1];
            Col[0] = Color.White;
            Recht.SetData(Col);
        }
        
        public void move_up()
        {
            if(this.Ypos > 0)
            {
                this.Ypos--;
            }
        }
        
        public void move_down()
        {
            if(this.Ypos < gridheight-1)
            {
                this.Ypos++;
            }
        }
        
        public void move_left()
        {
            if(this.Xpos > 0)
            {
                this.Xpos--;
            }
        }
        
        public void move_right()
        {
            if(this.Xpos < gridwidth-1)
            {
                this.Xpos++;
            }
        }
        
        public bool collision(Rechteck obj)
        {
            if(this.Xpos == obj.Xpos && this.Ypos == obj.Ypos)
            {
                return true;
            }
            else return false;
        }
        
        public void reposition()
        {
            Random r = new Random();
            this.XPos = r.Next(0, 15);
            this.YPos = r.Next(0, 15);
        }
    }
}
