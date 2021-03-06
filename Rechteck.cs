﻿using System;
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

	Texture2D Recht;
	SpriteBatch sprite;
	Color color;
        protected int Xpos;
        protected int Ypos;
        int gridwidth;
        int gridheight;
	int baselength;
        public int lastmove;


        public Rechteck(int Xpos, int Ypos, int gridwidth, int gridheight, int baselen, SpriteBatch s, Color col)
        {
            this.Xpos = Xpos;
            this.Ypos = Ypos;
            this.gridwidth = gridwidth;
            this.gridheight = gridheight;
		this.baselength = baselen;
		this.sprite = s;
		this.color = col;
        }   



        public void Load(GraphicsDevice GD)
        {
            Recht = new Texture2D(GD, 1, 1);
            Recht.SetData(new Color[] {this.color});

        }

        public int[] get_pos()
        {
            return new int[] { Xpos, Ypos };
        }

	public void move(InputHandle input, Keys[] controlkeys)
	{
            if (input.wasKeyPressed(controlkeys[0]) )
            {
                this.move_up();
            }

            if (input.wasKeyPressed(controlkeys[1]) )
            {
                this.move_right();
            }

            if (input.wasKeyPressed(controlkeys[2]) ){

                this.move_down();
            }

            if (input.wasKeyPressed(controlkeys[3]) )
            {
                this.move_left();
            }
	}
        
        protected void move_up()
        {
            if(this.Ypos > 0)
            {
                this.Ypos--;
            }
        }
        
        protected void move_down()
        {
            if(this.Ypos < gridheight-1)
            {
                this.Ypos++;
            }
        }
        
        protected void move_left()
        {
            if(this.Xpos > 0)
            {
                this.Xpos--;
            }
        }
        
        protected void move_right()
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
            this.Xpos = r.Next(0, 15);
            this.Ypos = r.Next(0, 15);
        }

	public void draw()
	{
	    this.sprite.Begin();
        this.sprite.Draw(Recht, new Rectangle(Xpos * baselength, Ypos * baselength, baselength, baselength), this.color);
	    this.sprite.End();
	}
    }
}
