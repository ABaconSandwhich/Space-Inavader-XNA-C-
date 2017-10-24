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

namespace Space_Invaders
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //enum GameState {Start, Pause, Play, Help, Exit}
        //GameState currentGameState = GameState.Start;
        string gameState = "Start";

        Texture2D playerTexture;
        Texture2D invader1Texture, invader2Texture, invader3Texture;
        Texture2D bullet;
        Texture2D bonusTexture;
        Texture2D bStart, bStart2, bHelp, bHelp2, bExit, Bexit2, bBack, bBack2, bMenu, bMenu2, bPlay, bPlay2, bPaused; //Menu Items
        SpriteFont font;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player1;
        List<Invader> listInvader;
        //List<Bullet> player1.bulletList;
        List<Bullet> listEnemyBullet;
        Bonus bonus;
        Menu menu;
        Color shipcolor = Color.LimeGreen;
        double shootTimer = 0;
        double livesTimer = 0;
        double gameTimer = 0;
        double bonusTimer = 0;
        int score = 0;
        int lives = 3;
        Random random = new Random();

        SoundEffect shootSound;
        SoundEffect explosionSound;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {           
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.ApplyChanges();
            this.IsMouseVisible = true;
            base.Initialize(); 
            restart();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // this loads up all of the sprites and sounds that the game needs to use and adds them to their respected variables.
            playerTexture = Content.Load<Texture2D>("Player");
            invader1Texture = Content.Load<Texture2D>("Invader1");
            invader2Texture = Content.Load<Texture2D>("Invader2");
            invader3Texture = Content.Load<Texture2D>("Invader3");
            bullet = Content.Load<Texture2D>("bullet");
            bonusTexture = Content.Load<Texture2D>("bonus");

            shootSound = Content.Load<SoundEffect>("shoot");
            explosionSound = Content.Load<SoundEffect>("explosion");

            #region menu content
            bStart = Content.Load<Texture2D>("Menu/start");
            bStart2 = Content.Load<Texture2D>("Menu/startPressed");
            bHelp = Content.Load<Texture2D>("Menu/help");
            bHelp2 = Content.Load<Texture2D>("Menu/helpPressed");
            bExit = Content.Load<Texture2D>("Menu/exit");
            Bexit2 = Content.Load<Texture2D>("Menu/exitPressed");
            bBack = Content.Load<Texture2D>("Menu/back");
            bBack2 = Content.Load<Texture2D>("Menu/backPressed");
            bPlay = Content.Load<Texture2D>("Menu/play");
            bPlay2 = Content.Load<Texture2D>("Menu/playPressed");
            bMenu = Content.Load<Texture2D>("Menu/menu");
            bMenu2 = Content.Load<Texture2D>("Menu/menuPressed");
            bPaused = Content.Load<Texture2D>("Menu/Paused");
            font = Content.Load<SpriteFont>("Menu/Font");
            #endregion
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Gets the keyboard state and stores it in "Key"
            KeyboardState Key = Keyboard.GetState(); 
            
            // checks if the current game state is "Play"
            if (gameState == "Play")
            {
                #region main updates
                // This updates all the timers that the game needs while in the "Play" gamestate
                bonusTimer += gameTime.ElapsedGameTime.TotalSeconds; 
                shootTimer += gameTime.ElapsedGameTime.TotalSeconds;
                gameTimer += gameTime.ElapsedGameTime.TotalSeconds;
                livesTimer += gameTime.ElapsedGameTime.TotalSeconds;

                // Updates the player ship
                player1.Update(shipcolor);

                //updates the bonus invader
                bonus.Update();

                //this foreach loop updates all invaders and also checks to see if the invaders needs to move down the screen or wether to go left or right and tells them to do so
                int i = 0;
                int x = 0;
                foreach (Invader invader in listInvader)
                {

                    if (listInvader[i].position.X > 940)
                    {
                        x = 0;
                        foreach (Invader invaderx in listInvader)
                        {
                            listInvader[x].goLeft();
                            listInvader[x].goDown();
                            x++;
                        }
                    }
                    if (listInvader[i].position.X < 25)
                    {
                        x = 0;
                        foreach (Invader invaderx in listInvader)
                        {
                            listInvader[x].goRight();
                            listInvader[x].goDown();
                            x++;
                        }
                    }

                    listInvader[i].update(gameTime);
                    
                    //randomly selects invader to shoot after the timer has done.
                    int shootChance = 0;
                    if (shootTimer > 0.6)
                    {
                        shootChance = random.Next(0, listInvader.Count);
                        listInvader[shootChance].shoot(ref listEnemyBullet);
                        shootTimer = 0;
                    }
                    i++;
                }
                
                //this foreach loop updates all of the enemy bullets 
                i = 0;
                foreach (Bullet b in listEnemyBullet)
                {
                    listEnemyBullet[i].update();
                    i++;
                }

                // checks to see if there are no lives left and changes the game state to gameover
                if (lives <= 0)
                {
                    gameState = "Gameover";
                }

                // adds a new bonus invader every 15 seconds
                if (bonusTimer > 15)
                {
                    bonus = new Bonus(bonusTexture, new Vector2(-80, 15));
                    bonusTimer = 0;
                }

                // Checks to see if the escape key is pressed and then changes the gamestate to "Paused"
                if (Key.IsKeyDown(Keys.Escape))
                {
                    gameState = "Paused";
                }
                #endregion


                #region collision
                // these two for loops check to see if any of the player bullets have collided with an invader, if so, the invader and bullet are removed
                // and the score is increased by 100
                for (i = 0; i < listInvader.Count; i++)
                {
                    for (x = 0; x < player1.bulletList.Count; x++)
                    {
                        try
                        {
                            if (player1.bulletList[x].bulletCollision.Intersects(listInvader[i].collision))
                            {
                                listInvader.RemoveAt(i);
                                player1.bulletList.RemoveAt(x);
                                score += 100;
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            resetInvader();
                        }
                    }
                    try
                    {
                        //Checks to see if the any of the invaders postistions are past 500 Y, then resets and the player loses a life
                        if (listInvader[i].position.Y > 500)
                        {
                            lives -= 1;
                            explosionSound.Play();
                            resetInvader();
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {}
                }
                //if the lives timer is done, checks to see if any of the enemy bullets have hit the player, if so then a life is lost and the ship
                // is immune from damage for the next second
                if (livesTimer > 1)
                {
                    for (i = 0; i < listEnemyBullet.Count; i++)
                    {
                        if (listEnemyBullet[i].bulletCollision.Intersects(player1.collision))
                        {
                            lives -= 1;
                            explosionSound.Play();
                            shipcolor = Color.White;
                            livesTimer = 0;
                        }
                    }
                }
                if (livesTimer > 0.9)
                {
                    shipcolor = Color.LimeGreen;
                }

                //Checks to see if any of the player bullets have hit the bonus ship, if so, the score is increased by 1000 and the bonus invader is moved away
                try
                {
                    if (player1.bulletList[0].bulletCollision.Intersects(bonus.collision))
                    {
                        score += 1000;
                        bonus.collision.X = 10000;
                    }
                }
                catch (ArgumentOutOfRangeException) { }
                #endregion

                    
            }

            // checks to see if the game state is either "Start", "Help", "Paused" or"Gameover", then opens up the menu
            if ((gameState == "Start") || (gameState == "Help") || (gameState == "Paused") || gameState == "Gameover")
            {
                gameState = menu.Update(gameState, score);
            }

            // if the gamestate is "Restart" then is calls the restart method and changes the gamestate back to start
            if (gameState == "Restart")
            {
                restart();
                gameState = "Start";
            }

            // if the gamestate is exit, then it exits the game
            if (gameState == "Exit")
            {
                this.Exit();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // draws everything needed for the "Play" gamestate
            if (gameState == "Play")
            {
                player1.draw(spriteBatch);
                int i = 0;
                foreach (Invader invader in listInvader)
                {
                    listInvader[i].draw(spriteBatch);
                    i++;
                }
                i = 0;
                foreach (Bullet bullet in listEnemyBullet)
                {
                    listEnemyBullet[i].draw(spriteBatch);
                    i++;
                }
                bonus.Draw(spriteBatch);
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Lives: " + lives.ToString(), new Vector2(20, 20), Color.White);
                spriteBatch.DrawString(font, "Score: " + score.ToString(), new Vector2(850, 20), Color.White);
                spriteBatch.DrawString(font, "Time: " + gameTimer.ToString("0.00"), new Vector2(20, 40), Color.White);
                spriteBatch.End();
            }
            // Draws the menu when needed
            if ((gameState == "Start") || (gameState == "Help") || (gameState == "Paused") || gameState == "Gameover")
            {
                menu.Draw(spriteBatch);
            }
            
            base.Draw(gameTime);
        }

        // The restart method resets all of the variables to their origianl value
        public void restart()
        {
            menu = new Menu(font, bStart, bStart2, bHelp, bHelp2, bExit, Bexit2, bBack, bBack2, bMenu, bMenu2, bPlay, bPlay2, bPaused);

            listEnemyBullet = new List<Bullet>();
            player1 = new Player(playerTexture, bullet, new Vector2(200, 650), shipcolor, shootSound);
            player1.bulletList = new List<Bullet>();
            listInvader = new List<Invader>();
            score = 0;
            lives = 3;
            gameTimer = 0;
            bonus = new Bonus(bonusTexture, new Vector2(0, 1000));
            resetInvader();           
        }

        // This function initialises or resets all of the invaders , their posititions, textures and id, and adds them to the list "listInvader"
        public void resetInvader()
        {
            listInvader.Clear();
            for (int i = 0; i < 11; i++)
            {
                listInvader.Add(new Invader(invader1Texture, bullet, new Vector2(50 * i, 200), i));
                listInvader.Add(new Invader(invader1Texture, bullet, new Vector2(50 * i, 160), i));
                listInvader.Add(new Invader(invader2Texture, bullet, new Vector2(50 * i, 120), i));
                listInvader.Add(new Invader(invader2Texture, bullet, new Vector2(50 * i, 80), i));
                listInvader.Add(new Invader(invader3Texture, bullet, new Vector2(50 * i, 40), i));
            }
        }
    }
}
