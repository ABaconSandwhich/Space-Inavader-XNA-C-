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
    class Bullet
    {
        Texture2D bulletTexture;
        public Rectangle bulletCollision;
        Vector2 startpos;
        bool goDown;
        Color bulletColor;
        
        public Bullet(Texture2D _bulletTexture, Vector2 _startpos, bool _goDown, Color _bulletColor)
        {
            // initialises all of the variables needed for a bullet
            bulletTexture = _bulletTexture;
            startpos = _startpos;
            goDown = _goDown;
            bulletColor = _bulletColor;
            bulletCollision = new Rectangle((int)startpos.X + 25, (int)startpos.Y - 15, 4, 21);            
        }

        public void update()
        {
            //checks to see if the bullet is going up or down the screen and moves it accordingly
            if (goDown) 
            {
                bulletCollision.Y += 3;
            }
            else if (!goDown)
            {
                bulletCollision.Y -= 7;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            // Draws the bullet
            spriteBatch.Begin();
            spriteBatch.Draw(bulletTexture, bulletCollision, bulletColor);
            spriteBatch.End();
        }


    }
}
