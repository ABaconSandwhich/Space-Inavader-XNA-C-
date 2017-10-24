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
    class Invader
    {
        Texture2D invaderTexture;
        bool travelRight;
        Rectangle drawSource;

        public Vector2 position;
        public int ID;
        public Rectangle collision;

        double timer = 0;
        double downTimer = 0;
        int animationCount = 0;

        List<Bullet> bulletList;
        Texture2D bullet;


        public Invader(Texture2D _invaderTexture, Texture2D _bullet,Vector2 _position, int _ID)
        {
            // initisalises all the variables needed for each invader
            invaderTexture = _invaderTexture;
            position = _position;
            ID = _ID;
            bullet = _bullet;

            collision = new Rectangle((int)position.X, (int)position.Y, 70, 25);
            travelRight = true;
            drawSource = new Rectangle(0, 0, invaderTexture.Width / 2, invaderTexture.Height);
            bulletList = new List<Bullet>();
        }

        public void update(GameTime gameTime)
        {
            // updates the animation/move timer and the move down timer
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            downTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= 0.2)
            {
                // switches the source rectangle every 0.2 seconds to animate the invader
                if (animationCount == 1)
                {
                    drawSource = new Rectangle(0, 0, invaderTexture.Width / 2, invaderTexture.Height);
                    animationCount = 0;
                }
                else
                {
                    drawSource = new Rectangle(invaderTexture.Width / 2, 0, invaderTexture.Width / 2, invaderTexture.Height);
                    animationCount = 1;
                }
                
                // moves the invader either left or right every 0.2 seconds
                if (travelRight)
                {
                    position.X += 20;
                    collision.X += 20;
                }
                if (!travelRight)
                {
                    position.X -= 20;
                    collision.X -= 20;
                }
                // resets animation/move timer
                timer = 0;
            }

            
            int i = 0;
            foreach (Bullet bullet in bulletList)
            {

                bulletList[i].update();
                i++;

            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(invaderTexture, position, drawSource, Color.Yellow);
            spriteBatch.End();
        
        }

        //sets the travel right bool to true
        public void goRight()
        {
            travelRight = true;
        }

        //sets the travel right bool to faalse
        public void goLeft()
        {
            travelRight = false;
        }

        //moves the invader down
        public void goDown()
        {
            if (downTimer > 2)
            {
                collision.Y += 25;
                position.Y += 25;
                downTimer = 0;
            }

        }

        //adds a new bullet for the invader
        public void shoot(ref List<Bullet> listEnemyBullet)
        {
            listEnemyBullet.Add(new Bullet(bullet, position, true, Color.Yellow));
        }
    }
}
