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
    class Bonus
    {
        public Rectangle collision;
        Texture2D bonus;

        public Bonus(Texture2D _bonus, Vector2 pos)
        {
            // initalises all of the variables needed
            bonus = _bonus;

            collision = new Rectangle((int)pos.X, (int)pos.Y, 58, 25);
        }

        public void Update()
        {
            // moves the bonus invader accross the screen
            collision.X += 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // draws the bonus invader
            spriteBatch.Begin();
            spriteBatch.Draw(bonus, collision, Color.Red);
            spriteBatch.End();
        }
    }
}
