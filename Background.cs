using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Computer_Science_end_project
{
    class Background
    {
        private int windowWidth;
        private int windowHeight;
        private Texture2D texture;
        private Vector2 location;
        private Rectangle edge;
        

        public Vector2 Location { get => location; set => location = value; }
        public Texture2D Texture { get => texture; set => texture = value; }
        public Rectangle Edge { get => edge; set => edge = value; }

        public Background(bool Scaler)
        {
            windowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            windowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            location = new Vector2(0, 0);
            edge = new Rectangle(0, 0, windowWidth-windowWidth/3, windowHeight);
            if (!Scaler)
            {
                edge = new Rectangle(0, 0, windowWidth, windowHeight);
            }
            
        }

      
        public void LoadContent(ContentManager content,string name)
        {
            Texture = content.Load<Texture2D>(@name);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Edge, Color.White);
        }

        public int TextureChecking(Maze maze, Player ThePlayer)
        {
            int i = 0;
            if(maze._Mazegenerated[(int)ThePlayer._Positioninmaze.X+1,(int)ThePlayer._Positioninmaze.Y] == 0)
            {
                i = i + 7;
            }
            else
            {
                if(maze._Mazegenerated[(int)ThePlayer._Positioninmaze.X - 1, (int)ThePlayer._Positioninmaze.Y] == 0)
                {

                }
            }


            return 0;
        }

    }
}
