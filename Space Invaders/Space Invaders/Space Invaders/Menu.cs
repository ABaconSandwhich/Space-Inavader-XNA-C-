using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Space_Invaders
{
    class Menu
    {
        Texture2D start;
        Texture2D start2;
        Texture2D help;
        Texture2D help2;
        Texture2D exit;
        Texture2D exit2;
        Texture2D back;
        Texture2D back2;
        Texture2D menu;
        Texture2D menu2;
        Texture2D play;
        Texture2D play2;
        Texture2D paused;

        Texture2D currentStart;
        Texture2D currentHelp;
        Texture2D currentExit;
        Texture2D currentBack;
        Texture2D currentMenu;
        Texture2D currentPlay;
        SpriteFont font;

        Rectangle startRect;
        Rectangle helpRect;
        Rectangle exitRect;
        Rectangle backRect;
        Rectangle menuRect;
        Rectangle playRect;
        Rectangle pausedRect;

        string gameState;
        int score;

        public Menu(SpriteFont _font, Texture2D _start, Texture2D _start2, Texture2D _help, Texture2D _help2, Texture2D _exit, Texture2D _exit2, Texture2D _back, Texture2D _back2, Texture2D _menu, Texture2D _menu2, Texture2D _play, Texture2D _play2, Texture2D _paused)
        {
            //initalises all of the texture variables and their collision rectangles
            start = _start;
            start2 = _start2;
            help = _help;
            help2 = _help2;
            exit = _exit;
            exit2 = _exit2;
            back = _back;
            back2 = _back2;
            menu = _menu;
            menu2 = _menu2;
            play = _play;
            play2 = _play2;
            paused = _paused;
            font = _font;


            startRect = new Rectangle(400, 150, 192, 44);
            helpRect = new Rectangle(400, 250, 192, 44);
            exitRect = new Rectangle(400, 350, 192, 44);
            backRect = new Rectangle(600, 600, 192, 44);
            menuRect = new Rectangle(400, 400, 192, 44);
            playRect = new Rectangle(400, 300, 192, 44); 
            pausedRect = new Rectangle(367, 100, 256, 91);
        }

        public string Update(string _gameState, int _score)
        {
            score = _score; //gets the updates score
            gameState = _gameState; // gets the update gamestate;

            //gets the current mouse state
            MouseState mouse;
            mouse = Mouse.GetState();
            
            #region Start
            //updates the start menu
            if (gameState == "Start")
            {
                // checks to see if the mouse if hovered over the button and changes the button
                if (startRect.Contains(mouse.X, mouse.Y))
                {
                    currentStart = start2;
                    //checks to see if the mouse if clicked, then changes gamestate acordingly
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        return "Play";
                    }
                }
                else
                {
                    currentStart = start;
                }

                // checks to see if the mouse if hovered over the button and changes the button
                if (helpRect.Contains(mouse.X, mouse.Y))
                {
                    currentHelp = help2;
                    //checks to see if the mouse if clicked, then changes gamestate acordingly
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        return "Help";
                    }
                }
                else
                {
                    currentHelp = help;
                }

                // checks to see if the mouse if hovered over the button and changes the button
                if (exitRect.Contains(mouse.X, mouse.Y))
                {
                    currentExit = exit2;
                    //checks to see if the mouse if clicked, then changes gamestate acordingly
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        return "Exit";
                    }
                }
                else
                {
                    currentExit = exit;
                }
            }
            #endregion

            #region Help
            //updates the help menu
            if (gameState == "Help")
            {
                // checks to see if the mouse if hovered over the button and changes the button
                if (backRect.Contains(mouse.X, mouse.Y))
                {
                    //checks to see if the mouse if clicked, then changes gamestate acordingly
                    currentBack = back2;
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        return "Start";
                    }
                }
                else
                {
                    currentBack = back;
                }
            }
            #endregion

            #region Paused
            //updates the pause menu
            if (gameState == "Paused")
            {
                // checks to see if the mouse if hovered over the button and changes the button
                if (playRect.Contains(mouse.X, mouse.Y))
                {
                    currentPlay = play2;
                    //checks to see if the mouse if clicked, then changes gamestate acordingly
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        return "Play";
                    }
                }
                else
                {
                    currentPlay = play;
                }
                // checks to see if the mouse if hovered over the button and changes the button
                if (menuRect.Contains(mouse.X, mouse.Y))
                {
                    currentMenu = menu2;
                    //checks to see if the mouse if clicked, then changes gamestate acordingly
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        return ("Restart");
                    }
                }
                else
                {
                    currentMenu = menu;
                }
            }
            #endregion

            #region Gameover
            //updates the gameover screen           
            if (gameState == "Gameover")
            {
                if (menuRect.Contains(mouse.X, mouse.Y))
                {
                    currentMenu = menu2;
                    //checks to see if the mouse if clicked, then changes gamestate acordingly
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        gameState = "Restart";
                    }
                }
                else
                {
                    currentMenu = menu;
                }
            }
            #endregion

            return gameState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //draws everything needed for each of the menus when the current gamestate says too
            
            if (gameState == "Start")
            {
                spriteBatch.Begin();
                spriteBatch.Draw(currentStart, startRect, Color.White);
                spriteBatch.Draw(currentHelp, helpRect, Color.White);
                spriteBatch.Draw(currentExit, exitRect, Color.White);
                spriteBatch.End();
            }
            if (gameState == "Help")
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font,"Use the arrow keys to move your ship left and right", new Vector2(50, 50), Color.White);
                spriteBatch.DrawString(font, "Press space bar to shoot", new Vector2(50, 75), Color.White);
                spriteBatch.DrawString(font, "Press Escape to pause the game", new Vector2(50, 100), Color.White);
                spriteBatch.DrawString(font, "Shoot all the invaders before they get to the bottom of the screen or you lose a life", new Vector2(50, 125), Color.White);
                spriteBatch.DrawString(font, "If the yellow bullets hit you then you lose a life", new Vector2(50, 150), Color.White);
                spriteBatch.DrawString(font, "Shoot the red ship to get extra points", new Vector2(50, 175), Color.White);
                spriteBatch.DrawString(font, "If the ammount of lives you have reach zero then it is game over", new Vector2(50, 200), Color.White);
                spriteBatch.Draw(currentBack, backRect, Color.White);
                spriteBatch.End();
            }
            if (gameState == "Paused")
            {
                spriteBatch.Begin();
                spriteBatch.Draw(paused, pausedRect, Color.White);
                spriteBatch.Draw(currentPlay, playRect, Color.White);
                spriteBatch.Draw(currentMenu, menuRect, Color.White);
                spriteBatch.End();
            }
            if (gameState == "Gameover")
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Your score was: " + score, new Vector2(400, 280), Color.White);
                spriteBatch.Draw(currentMenu, menuRect, Color.White);
                spriteBatch.End();
            }

        }
    }
}
