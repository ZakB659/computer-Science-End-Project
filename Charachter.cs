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
    public enum direction
    {
        None,
        North,
        East,
        South,
        West,

    }  


    class Charachter
    {
        private Vector2 Location;
        private Texture2D Texture;
        private Rectangle HitBox;
        private int size;
        private int health;
        private int MovementSpeed;
        private int windowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        private int windowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        private int windowWidthdiv3 = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/3;
        private int windowHeightdiv2 = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height/2;

        public Texture2D _Texture { get => Texture; set => Texture = value; }
        public int _Size { get => size; set => size = value; }
        public Vector2 _Location { get => Location; set => Location = value; }
        public int Health { get => health; set => health = value; }
        public int _MovementSpeed { get => MovementSpeed; set => MovementSpeed = value; }
        public Rectangle _HitBox { get => HitBox; set => HitBox = value; }
        public int WindowWidth { get => windowWidth; set => windowWidth = value; }
        public int WindowHeight { get => windowHeight; set => windowHeight = value; }
        public int WindowWidthdiv3 { get => windowWidthdiv3; set => windowWidthdiv3 = value; }
        public int WindowHeightdiv2 { get => windowHeightdiv2; set => windowHeightdiv2 = value; }

        public virtual void loadcontent(ContentManager content,string name)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
