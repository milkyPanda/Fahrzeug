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
	class Button: SpriteBatch
	{
		int[] pos;
		int width;
		int height;
		Color activecolor;
		Color inactivecolor;
		Color color_in_use;
		bool active;

		public Button(int[] pos, int w, int h, Color active, Color inactive, bool a = false)
		:base(GraphicsDevice)
		{
			this.pos = pos;
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
			color
		}

		public void deactivate()
		{
			active = false;
		}

		public bool is_active()
		{
			return active;
		}

		public void draw()
		{
			Texture2D texture = new Texture2D(GraphicsDevice, this.width, this.height);
			texture.setData(new Color[] {color_in_use} );
			base.Draw(texture, new Rectangle(pos[0], pos[1], width, height), color_in_use);
		}
	}
}
