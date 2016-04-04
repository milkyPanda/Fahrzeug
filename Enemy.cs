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
    class Enemy: Rechteck
    {
        int Wuerste;
        int closest;
        int direction;

        public Enemy(int Xpos, int Ypos, int gridwidth, int gridheight, int baselen, SpriteBatch s, Color col, int Wuerste)
            :base(Xpos, Ypos, gridwidth, gridheight, baselen, s, col)
        {
            this.Wuerste = Wuerste;
            this.closest = 0;
            this.direction = 1;
        }

        public void move(int[][] pos)
        {
            if (Wuerste == 1)
            {
                closest = 0;
            }
            else
            {
                //calculate which player is closer and needs to be chased
                int dist0 = Formeln.distance(this.Xpos - pos[0][0], this.Ypos - pos[0][1]);
                int dist1 = Formeln.distance(this.Xpos - pos[1][0], this.Ypos - pos[1][1]);

                if (dist0 < dist1)
                {
                    closest = 0;
                }
                else
                {
                    closest = 1;
                }
            }

            //move the this
            if (direction == 1)
            {
                if (this.Xpos < pos[closest][0])
                {
                    this.move_right();
                }
                else if (this.Xpos > pos[closest][0])
                {
                    this.move_left();
                }

                direction = 2;
            }

            if (direction == 2)
            {
                if (this.Ypos < pos[closest][1])
                {
                    this.move_down();
                }

                else if (this.Ypos > pos[closest][1])
                {
                    this.move_up();
                }

                direction = 1;
            }
        }
    }
}
