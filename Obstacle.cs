using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Computer_Science_end_project
{
    class Obstacle : Charachter
    {
        private Texture2D Texture;
        private Vector2 location;

        public Vector2 Location { get => location; set => location = value; }

        public Obstacle()
        {

        }



        public override void loadcontent(ContentManager content, string name)
        {
            base.loadcontent(content, name);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
