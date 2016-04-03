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
        Rechteck Coin;
        Grid gamegrid;
        int gridwidth;
        int gridheight;
        int Wurstbase = 20;
        bool is_draw_needed = true;
        int coincounter = 0;


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
            this.Wurst = new Rechteck(14,14,gridwidth,gridheight);

            this.Coin = new Rechteck(0, 0,gridwidth,gridheight);
            Wurst.Load(GraphicsDevice,Color.White);
            Coin.Load(GraphicsDevice,Color.Yellow);


        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            es_mi_regal.Update();
            
            //Check if the Wurst needs to be moved
            if (es_mi_regal.wasKeyPressed(Keys.W) )
            {
                Wurst.move_up();
            }

            if (es_mi_regal.wasKeyPressed(Keys.S) )
            {
                Wurst.move_down();
            }

            if (es_mi_regal.wasKeyPressed(Keys.A) ){
            
                Wurst.move_left();
            }

            if (es_mi_regal.wasKeyPressed(Keys.D) )
            {
                Wurst.move_right();
            }
            


            if( Wurst.collision(Coin) )

            {
                Coin.reposition();
                coincounter++;
            }
            
            
            if (coincounter >= 10)
            {
                is_draw_needed = false;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Rectangle Size = new Rectangle(gamegrid.get_x_pixel_pos(Wurst.get_Xpos()), gamegrid.get_y_pixel_pos(Wurst.get_Ypos()), Wurstbase, Wurstbase);
            Rectangle grid = new Rectangle(0, 0, gamegrid.pixWidth(), gamegrid.pixHeight());

            if (is_draw_needed == true)
            {
                //Draw the grid
                spriteBatch.Begin();
                spriteBatch.Draw(gamegrid.get_grid(), grid, Color.Black);
                spriteBatch.Draw(Coin.Recht, new Rectangle(gamegrid.get_x_pixel_pos(Coin.get_Xpos()), gamegrid.get_y_pixel_pos(Coin.get_Ypos()), Wurstbase, Wurstbase), Color.Yellow);
                spriteBatch.Draw(Wurst.Recht, Size, Color.White);
                spriteBatch.End();
            }
            else
            {
                GraphicsDevice.Clear(Color.Green);
            }
            base.Draw(gameTime);
        }
    }
}
