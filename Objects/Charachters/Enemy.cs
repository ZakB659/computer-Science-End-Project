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
    class Enemy : Charachter
    {
       
        public direction Direction;
        private Vector2 movement;        
        public int difficulty;
        public bool IsRemoved = false;
        public int movinganimationX;
        public int movinganimationY;
        public double speed;
       

        public Vector2 Movement { get => movement; set => movement = value; }
      

        public Enemy(int floor,double multiplier)
        {
            WindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            WindowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Random RNG = new Random();

            speed = 1 / multiplier;
             
            difficulty = floor;           

            Health = difficulty * 10 / 2;
            movinganimationY = 0;
            movinganimationX = 0;
            switch (RNG.Next(1, 5))
            {
                case 1:
                    Direction = direction.North;
                    _Location = new Vector2(WindowWidth / 3 + (32 * (RNG.Next(0, 2) - 1)), WindowHeight);
                    break;
                case 2:
                    Direction = direction.East;
                    _Location = new Vector2(0, WindowHeight / 2 + (32 * (RNG.Next(0, 2) - 1)));
                    break;
                case 3:
                    Direction = direction.South;
                    _Location = new Vector2(WindowWidth / 3 + (32 * (RNG.Next(0, 2) - 1)), 0);
                    break;
                case 4:
                    Direction = direction.West;
                    _Location = new Vector2(2*WindowWidth/3, WindowHeight / 2 + (32 * (RNG.Next(0, 2) - 1)));
                    break;
            }         
            

            switch (floor)
            {
                case int n when (n >= 1 && n <= 3):
                    movinganimationY = 0;
                    break;
                case int n when (n >= 4 && n <= 6):
                    movinganimationY = 1;
                    break;
                case int n when (n >= 7 && n <= 9):
                    movinganimationY = 2;
                    break;
                default:
                    movinganimationY = 3;
                    break;
            }
        }

        public override void loadcontent(ContentManager content,string name)
        {
            _Texture = content.Load<Texture2D>(@name);
        }

        public void update(GameTime gameTime,Player player)
        {
           // Turn into the a* algorithm 
            movement.X = 0;
            movement.Y = 0;

            switch (Direction)
            {
                case direction.South:
                    movement.Y = 1*((float)speed);
                    
                    movinganimationX = 0;
                    break;
                case direction.North:
                    movement.Y = -1 * ((float)speed);
                    
                    movinganimationX = 2;
                    break;
                case direction.West:
                    movement.X = -1 * ((float)speed);
                    
                    movinganimationX = 3;
                    break;
                case direction.East:
                    movement.X = 1 * ((float)speed);
                    
                    movinganimationX = 1;
                    break;
            }
            _Location = _Location + movement;
            _HitBox = new Rectangle((int)_Location.X, (int)_Location.Y, 32, 64);

            if (Health < 0)
            {
                IsRemoved = true;
            }
        }

        public void astar()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            

            if ((_Location.X < 2 * WindowWidth / 3 && _Location.X > 0) && (_Location.Y < WindowHeight && _Location.Y > 0))
            {
                spriteBatch.Draw(_Texture, _Location, new Rectangle(32 * movinganimationX, 32 * movinganimationY, 32, 32), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
            }
            else
            {
                IsRemoved = true;
            }
        }
    }
}
