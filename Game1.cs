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
        Rechteck enemy;
        Grid gamegrid;
        int gridwidth;
        int gridheight;
        int Wurstbase = 20;
        bool win = false;
        bool lose = false;
        int coincounter = 0;
        int framecounter = 0;
        int dirrection = 1;

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
            
            //create and load wurst
            this.Wurst = new Rechteck(14,14,gridwidth,gridheight);
            Wurst.Load(GraphicsDevice,Color.White);

            //create and load coin
            Random r = new Random();
            this.Coin = new Rechteck(r.Next(0, 15), r.Next(0, 15), gridwidth, gridheight);
            Coin.Load(GraphicsDevice, Color.Yellow);
            
            //create and load enemy
            this.enemy = new Rechteck(0, 0, gridwidth, gridheight);
            enemy.Load(GraphicsDevice, Color.Red);
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



            //move the enemy 3 times per second
            if(framecounter == 30)
            {
                if (dirrection == 1)
                {
                    if (enemy.get_Xpos() < Wurst.get_Xpos())
                    {
                        enemy.move_right();
                    }
                    else if (enemy.get_Xpos() > Wurst.get_Xpos())
                    {
                        enemy.move_left();
                    }
                    dirrection = 2;
                }

                if(dirrection == 2)
                {
                    if (enemy.get_Ypos() < Wurst.get_Ypos())
                    {
                        enemy.move_down();
                    }
                    else if (enemy.get_Ypos() > Wurst.get_Ypos())
                    {
                        enemy.move_up();
                    }
                    dirrection = 1;
                }



                framecounter = 0;
            }
            else
            {
                framecounter++;
            }

            if( Wurst.collision(Coin) )

            {
                Coin.reposition();
                coincounter++;
            }

            if (Wurst.collision(enemy))
            {
                Coin.reposition();
                lose = true;
            }
            
            if (coincounter >= 10)
            {
                win = true;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!lose && !win)
            {
                spriteBatch.Begin();
                //draw grid
                spriteBatch.Draw(gamegrid.get_grid(), new Rectangle(0, 0, gamegrid.pixWidth(), gamegrid.pixHeight()), Color.Black);
                //draw coin
                spriteBatch.Draw(Coin.Recht, new Rectangle(gamegrid.x_pixpos(Coin.get_Xpos()), gamegrid.y_pixpos(Coin.get_Ypos()), Wurstbase, Wurstbase), Color.Yellow);
                //draw wurst
                spriteBatch.Draw(Wurst.Recht, new Rectangle(gamegrid.x_pixpos(Wurst.get_Xpos()), gamegrid.y_pixpos(Wurst.get_Ypos()), Wurstbase, Wurstbase), Color.White);
                //draw enemy
                spriteBatch.Draw(enemy.Recht, new Rectangle(gamegrid.x_pixpos(enemy.get_Xpos()), gamegrid.y_pixpos(enemy.get_Ypos()), Wurstbase, Wurstbase), Color.Red);
                spriteBatch.End();
            }
            else if (lose == true)
            {
                GraphicsDevice.Clear(Color.Red);
            }
            else
            {
                GraphicsDevice.Clear(Color.Green);
            }

            base.Draw(gameTime);
        }
    }
}
