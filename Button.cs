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
	class Button
	{
        GraphicsDevice gd;
        SpriteBatch sprite;
		int[] pos;
		int width;
		int height;
		Color activecolor;
		Color inactivecolor;
		Color color_in_use;
		bool active;

		public Button(int[] pos, int w, int h, Color active, Color inactive, GraphicsDevice gd, SpriteBatch sprite, bool a = false)
		{
            this.gd = gd;
            this.sprite = sprite;
			this.pos = new int[] {pos[0], pos[1]};
			this.width = w;
			this.height = h;
			this.activecolor = active;
			this.inactivecolor = inactive;
			this.active = a;
			if(a)
			{
				color_in_use = activecolor;
			}
			else
			{
				color_in_use = inactivecolor;
			}
		}

		public void activate()
		{
			active = true;
            color_in_use = activecolor;
		}

		public void deactivate()
		{
			active = false;
            color_in_use = inactivecolor;
		}

		public bool is_active()
		{
			return active;
		}

		public void draw()
		{
			Texture2D texture = new Texture2D(gd, 1, 1);
			texture.SetData(new Color[] {color_in_use} );
            sprite.Begin();
			sprite.Draw(texture, new Rectangle(pos[0], pos[1], width, height), color_in_use);
            sprite.End();
		}
	}
}
