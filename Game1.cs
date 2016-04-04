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
        Rechteck[] Wurst;
	Keys[][] keys;
        Rechteck Coin;
        Rechteck enemy;
        Grid gamegrid;
        int gridwidth;
        int gridheight;
        int Wurstbase = 20;
        bool[] win;
        int[] coincounter;
        int framecounter = 0;
        int dirrection = 1;
        int chosen_wurst = 0;
        int enemy_to_player0;
        int enemy_to_player1;

        public Game1()
        {
            gridwidth = 15;
            gridheight = 15;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Wurstbase * gridheight;
            graphics.PreferredBackBufferWidth = Wurstbase * gridwidth;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

	        keys = new Keys[2][];
	        keys[0] = new Keys[] {Keys.W, Keys.D, Keys.S, Keys.A};
	        keys[1] = new Keys[] {Keys.Up, Keys.Right, Keys.Down, Keys.Left};
        
	        coincounter = new int[] {0, 0};

            win = new bool[] {false, false};
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
            this.Wurst = new Rechteck[2];
	        this.Wurst[0] = new Rechteck(14, 14, gridwidth, gridheight, Wurstbase, spriteBatch, Color.White);
	        this.Wurst[1] = new Rechteck(7, 7, gridwidth, gridheight, Wurstbase, spriteBatch, Color.Blue);
            Wurst[0].Load(GraphicsDevice);
            Wurst[1].Load(GraphicsDevice);

            //create and load coin
            Random r = new Random();
            this.Coin = new Rechteck(r.Next(0, 15), r.Next(0, 15), gridwidth, gridheight, Wurstbase, spriteBatch, Color.Yellow);
            Coin.Load(GraphicsDevice);
            
            //create and load enemy
            this.enemy = new Rechteck(r.Next(0, 15), r.Next(0, 15), gridwidth, gridheight, Wurstbase, spriteBatch, Color.Red);
            enemy.Load(GraphicsDevice);
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
	        Wurst[0].move(es_mi_regal, keys[0]);
	        Wurst[1].move(es_mi_regal, keys[1]);

            enemy_to_player0 = Formeln.distance(enemy.get_Xpos() - Wurst[0].get_Xpos(), enemy.get_Ypos() - Wurst[0].get_Ypos());
            enemy_to_player1 = Formeln.distance(enemy.get_Xpos() - Wurst[1].get_Xpos(), enemy.get_Ypos() - Wurst[1].get_Ypos());

            if (enemy_to_player0 < enemy_to_player1)
            {
                chosen_wurst = 0;
            }
            else
            {
                chosen_wurst = 1;
            }


            if(!win[0] && !win[1])
            {
                //move the enemy 3 times per second
                if(framecounter == 20)
                {
                    if (dirrection == 1)
                    {
                        if (enemy.get_Xpos() < Wurst[chosen_wurst].get_Xpos())

                        {
                            enemy.move_right();
                        }
                        else if (enemy.get_Xpos() > Wurst[chosen_wurst].get_Xpos())
                        {
                            enemy.move_left();
                        }

                        dirrection = 2;
                    }

                    if(dirrection == 2)
                    {
                        if (enemy.get_Ypos() < Wurst[chosen_wurst].get_Ypos())

                        {
                            enemy.move_down();
                        }

                        else if (enemy.get_Ypos() > Wurst[chosen_wurst].get_Ypos())

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
            }

	        //check if either Wurst collided with coin
	        if(Wurst[0].collision(Coin) )
	        {
		        Coin.reposition();
		        coincounter[0]++;
	        }
            if( Wurst[1].collision(Coin) )
            {
                Coin.reposition();
                coincounter[1]++;
            }

            if (!win[0] && !win[1])
            {
                //check if either Wurst collided with enemy
                if (Wurst[0].collision(enemy))
                {
                    win[1] = true;
                }
                if (Wurst[1].collision(enemy))
                {
                    win[0] = true;
                }
            }
            
            if (coincounter[0] >= 5)
            {
                win[0] = true;
            }
	        if (coincounter[1] >= 5)
	        {
		        win[1] = true;
	        }

	        //Update gameTime in base class
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!win[0] && !win[1])
            {
		        //draw grid
		        spriteBatch.Begin();
                spriteBatch.Draw(gamegrid.get_grid(), new Rectangle(0, 0, gamegrid.pixWidth(), gamegrid.pixHeight()), Color.Black);
		        spriteBatch.End();

                //draw coin
		        Coin.draw();

                //draw wurst
		        Wurst[0].draw();
		        Wurst[1].draw();

                //draw enemy
		        enemy.draw();
            }
            else if (win[0])   //player 0 won
            {
                GraphicsDevice.Clear(Color.Green);
                Wurst[0].draw();
            }
            else       //player 1 won
            {
                GraphicsDevice.Clear(Color.Green);
                Wurst[1].draw();
            }

            base.Draw(gameTime);
        }
    }
}
