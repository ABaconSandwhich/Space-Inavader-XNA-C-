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
    class Player
    {
        Texture2D playerTexture;
        Texture2D bullet;
        SoundEffect shootSound;
        Vector2 position;
        public List<Bullet> bulletList;
        Color shipColor;
        public Rectangle collision;

        public Player(Texture2D _playerTexture, Texture2D _bullet, Vector2 _position, Color _shipColor, SoundEffect _shootSound)
        {
            //initilises all of the variables needed for the player
            playerTexture = _playerTexture;
            position = _position;
            bullet = _bullet;
            shipColor = _shipColor;
            shootSound = _shootSound;
            bulletList = new List<Bullet>();
            collision = new Rectangle((int)position.X, (int)position.Y, 54, 33);
        }

        public void Update(Color _shipColor)
        {
            shipColor = _shipColor; //gets the updated ship color

            //gets the state of the keyboard
            KeyboardState Key = Keyboard.GetState();
            //moves the player left or right depending on what key is pressed
            if ((Key.IsKeyDown(Keys.Right)) && (position.X < 940))
            {
                position.X += 5;
                collision.X += 5;
            }
            if ((Key.IsKeyDown(Keys.Left)) && (position.X > 5))
            {
                position.X -= 5;
                collision.X -= 5;
            }

            // shoots a bullet if space is pressed and there isnt already a player fired bullet on the screen
            if (Key.IsKeyDown(Keys.Space))
            {
                if (bulletList.Count < 1)
                {
                    bulletList.Add(new Bullet(bullet, position, false, Color.LimeGreen));
                    shootSound.Play();
                }
            }
            // checks to see if the bullet has hit the top of the screen and then removes it
            int i = 0;
            foreach (Bullet bullet in bulletList)
            {
                
                bulletList[i].update();
                if (bulletList[i].bulletCollision.Y < 0)
                {
                    bulletList.RemoveAt(i);
                    break;
                }
                i++;
              
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            //draws the player and each of the fired bullets
            spriteBatch.Begin();
            spriteBatch.Draw(playerTexture, collision, shipColor);
            spriteBatch.End();
            int i = 0;
            foreach (Bullet bullet in bulletList)
            {
                bulletList[i].draw(spriteBatch);
                i++;
            }
        }
    }
}
