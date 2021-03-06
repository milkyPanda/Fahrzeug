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
		int windowwidth;
		int windowheight;

        InputHandle es_mi_regal;

		Button singlep;
		Button multip;

        Grid gamegrid;
        Rechteck[] Wurst;
		Keys[][] keys;
        Rechteck Coin;
        Enemy enemy;

        int gridwidth;
        int gridheight;
        int Wurstbase = 20;

        bool[] win;
        int[] coincounter;
        int framecounter = 0;

        int chosen_wurst = 0;
        int enemy_to_player0;
        int enemy_to_player1;

		int gamemode = 0;		//0 = startscreen, 1 = singleplayer, 2 = multiplayer

        public Game1()
        {
            gridwidth = 15;
			windowwidth = gridwidth * Wurstbase;
            gridheight = 15;
			windowheight = gridheight * Wurstbase;
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

			//create buttons
			int[] pos = new int[2];
			pos[0] = (windowwidth-100) / 2;
			pos[1] = windowheight/2 - windowheight/6 - 20;
			singlep = new Button(pos, 100, 20, Color.LightGreen, Color.Gray, GraphicsDevice, spriteBatch, true);
			pos[1] = windowheight/2 + windowheight/6;
			multip = new Button(pos, 100, 20, Color.LightGreen, Color.Gray, GraphicsDevice, spriteBatch);

            //create and load wurst
            this.Wurst = new Rechteck[2];
	        this.Wurst[0] = new Rechteck(0, 0, gridwidth, gridheight, Wurstbase, spriteBatch, Color.White);
	        this.Wurst[1] = new Rechteck(14, 14, gridwidth, gridheight, Wurstbase, spriteBatch, Color.Blue);
            Wurst[0].Load(GraphicsDevice);
            Wurst[1].Load(GraphicsDevice);

            //create and load coin
            Random r = new Random();
            this.Coin = new Rechteck(r.Next(0, 15), r.Next(0, 15), gridwidth, gridheight, Wurstbase, spriteBatch, Color.Yellow);
            Coin.Load(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

			//update Input data
            es_mi_regal.Update();
            

			if(gamemode == 0)		//start screen
			{
				if(es_mi_regal.wasKeyPressed(Keys.Up) && !singlep.is_active() )
				{
					singlep.activate();
					multip.deactivate();
				}
				else if(es_mi_regal.wasKeyPressed(Keys.Down) && !multip.is_active() )
				{
					multip.activate();
					singlep.deactivate();
				}
				else if(es_mi_regal.wasKeyPressed(Keys.Enter) )
				{
					if(singlep.is_active() )
					{
						gamemode = 1;

                        //create and load enemy
                        this.enemy = new Enemy(7, 7, gridwidth, gridheight, Wurstbase, spriteBatch, Color.Red, 1);
                        enemy.Load(GraphicsDevice);
					}
					else if(multip.is_active() )
					{
						gamemode = 2;

                        //create and load enemy
                        this.enemy = new Enemy(7, 7, gridwidth, gridheight, Wurstbase, spriteBatch, Color.Red, 2);
                        enemy.Load(GraphicsDevice);
					}
					else
					{
						//ERROR
					}
				}
			}	//end start screen

			if(gamemode == 1)		//singleplayer mode
			{
				//update first Wurst
				Wurst[0].move(es_mi_regal, keys[0]);

				if(!win[0])
		        {
		            //move the enemy 2 times per second
		            if(framecounter == 30)
		            {
                        enemy.move(new int[][] {Wurst[0].get_pos(), new int[2] } );
		                framecounter = 0;
		            }
		            else
		            {
		                framecounter++;
		            }

					//check if Wurst collided with coin
					if(Wurst[0].collision(Coin) )
					{
						Coin.reposition();
						coincounter[0]++;
					}

		            //check if Wurst collided with enemy
		            if (Wurst[0].collision(enemy))
		            {
		                win[1] = true;
		            }

					//check if Wurst won
				    if (coincounter[0] >= 2)
				    {
				        win[0] = true;
				    }
		        }	//end !win
			}	//end singleplayer mode

			if(gamemode == 2)		//multiplayer mode
			{
		        //Check if either Wurst needs to be moved
				Wurst[0].move(es_mi_regal, keys[0]);
				Wurst[1].move(es_mi_regal, keys[1]);

				if(!win[0] && !win[1])
		        {
					//move the enemy 2 times per second
                    if(framecounter == 30)
                    {
                       enemy.move(new int[][] {Wurst[0].get_pos(), Wurst[1].get_pos()} );
				       framecounter = 0;
                    }
                    else
                    {
                        framecounter++;
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

					//check if either Wurst collided with enemy
		            if (Wurst[0].collision(enemy))
		            {
		                win[1] = true;
		            }
		            if (Wurst[1].collision(enemy))
		            {
		                win[0] = true;
		            }

					//check if either Wurst won
					if (coincounter[0] >= 2)
					{
					    win[0] = true;
					}
					if (coincounter[1] >= 2)
					{
						win[1] = true;
					}

		        }	//end !win
			}	//end multiplayer mode

	        //Update gameTime in base class
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
			if(gamemode == 0)
			{
				singlep.draw();
				multip.draw();
			}
			else if( (gamemode != 0 && !win[0]) || (gamemode == 2 && !win[0] && !win[1]) )
            {
				//draw grid
	        	spriteBatch.Begin();
                spriteBatch.Draw(gamegrid.get_grid(), new Rectangle(0, 0, gamegrid.pixWidth(), gamegrid.pixHeight()), Color.Black);
		        spriteBatch.End();

	            //draw coin
		        Coin.draw();

				//draw Wurst
	        	Wurst[0].draw();

                //draw enemy
		        enemy.draw();

				if(gamemode == 2)
				{
					//draw second Wurst
		        	Wurst[1].draw();
				}
            }
            if (win[0])   //player 0 won
            {
                GraphicsDevice.Clear(Color.Green);
                Wurst[0].draw();
            }
            else if(win[1])	//player 1 won
            {
                GraphicsDevice.Clear(Color.Green);
                Wurst[1].draw();
            }
			else
			{
				//ERROR
			}

            base.Draw(gameTime);
        }
    }
}
