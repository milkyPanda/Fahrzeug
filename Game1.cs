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

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputHandle es_mi_regal;
        Rechteck Wurst;
        Grid gamegrid;
        int gridwidth;
        int gridheight;
        int Wurstbase = 20;
        

        public Game1()
        {
            gridwidth = 15;
            gridheight = 15;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Wurstbase * gridheight;
            graphics.PreferredBackBufferWidth = Wurstbase * gridwidth;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }


        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.gamegrid = new Grid(GraphicsDevice, gridwidth, gridheight, Wurstbase);
            this.es_mi_regal = new InputHandle();
            this.Wurst = new Rechteck(14,14);
            Wurst.Load(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            es_mi_regal.Update();

            if (es_mi_regal.wasKeyPressed(Keys.W) && Wurst.Ypos != 0)
            {
                Wurst.Ypos--;
            }

            if (es_mi_regal.wasKeyPressed(Keys.S) && Wurst.Ypos < gridwidth-1)
            {
                Wurst.Ypos++;
            }

            if (es_mi_regal.wasKeyPressed(Keys.A) && Wurst.Xpos != 0)
            {
                Wurst.Xpos--;
            }

            if (es_mi_regal.wasKeyPressed(Keys.D) && Wurst.Xpos < gridheight-1)
            {
                Wurst.Xpos++;
            }
            
            //check if Wurst hits the coin
            if( Wurst.XPos == Coin.Xpos && Wurst.YPos == Coin.YPos)
            {
                Coin.reposition();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Rectangle Size = new Rectangle(gamegrid.get_x_pixel_pos(Wurst.Xpos),gamegrid.get_y_pixel_pos(Wurst.Ypos),Wurstbase,Wurstbase);
            Rectangle grid = new Rectangle(0, 0, gamegrid.pixWidth(), gamegrid.pixHeight());
            
            //Draw the grid
            spriteBatch.Begin();
            spriteBatch.Draw(gamegrid.get_grid(), grid, Color.Black);

            spriteBatch.Draw(Rechteck.Recht,Size, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
