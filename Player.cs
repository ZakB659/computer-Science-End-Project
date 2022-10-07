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
    class Player : Charachter
    {
        private Vector2 Movement;
        private Vector2 Roomchange;
        private Vector2 projectile_Movement;
        private Vector2 Positioninmaze;
        private Vector2 ResetVector = new Vector2(0,0);
        private direction Direction;
        private bool movingroom;
        private bool MovedRoom;
        private bool canshoot;      
        public int lives;
        public int movinganimationX;
        private int shootingspeed;
        private double timetowait;
        public double Timewaited;
        public bool CanMoveRoom;
        private bool entered_Combat_Room = false;
        


        public Vector2 _Movement { get => Movement; set => Movement = value; }
        public direction _Direction { get => Direction; set => Direction = value; }
        public int Shootingspeed { get => shootingspeed; set => shootingspeed = value; }
        public double Timetowait { get => timetowait; set => timetowait = value; }
        public Vector2 _Positioninmaze { get => Positioninmaze; set => Positioninmaze = value; }
        public bool Movingroom { get => movingroom; set => movingroom = value; }
        public bool MovedRoom1 { get => MovedRoom; set => MovedRoom = value; }
        public bool Entered_Combat_Room { get => entered_Combat_Room; set => entered_Combat_Room = value; }
        public Vector2 Projectile_Movement { get => projectile_Movement; set => projectile_Movement = value; }
        public bool Canshoot { get => canshoot; set => canshoot = value; }

        public Player()
        {            
            lives = 3;
            Direction = direction.None;
            _Location = new Vector2(WindowWidthdiv3, WindowHeightdiv2);
            Positioninmaze = new Vector2(1, 3);
            _MovementSpeed = 5;
            Health = 10;
            Timetowait = 30;
            Timewaited = 30;
            shootingspeed = 2;
            Movingroom = false;
        }

        public void Update(GameTime time, Projectiles projectiles, ContentManager content, Player Theplayer,Room ThisRoom)
        {
            KeyboardState ks = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            _HitBox = new Rectangle((int)_Location.X, (int)_Location.Y, 32, 32);
            CanMoveRoom = false;

            do
            {
                foreach (Rectangle BorderHitbox in ThisRoom._Borders)
                {
                    if (_HitBox.Intersects(BorderHitbox))
                    {
                        CanMoveRoom = true;
                    }
                    
                }
                break;
            } while (CanMoveRoom == false);

            

            if (CanMoveRoom == true)
            {
                Movement = ResetVector;
                Projectile_Movement = ResetVector;
                
                if (Movingroom == true)
                {
                    MoveroomAnimation();
                }
                else
                {
                    if (ks.IsKeyDown(Keys.W))
                    {
                        Movement.Y = _MovementSpeed * (Movement.Y - 1);
                        _Direction = direction.North;
                        movinganimationX = 3;
                    }
                    if (ks.IsKeyDown(Keys.A))
                    {
                        Movement.X = _MovementSpeed * (Movement.X - 1);
                        _Direction = direction.West;
                        movinganimationX = 1;
                    }
                    if (ks.IsKeyDown(Keys.D))
                    {
                        Movement.X = _MovementSpeed * (Movement.X + 1);
                        _Direction = direction.East;
                        movinganimationX = 2;
                    }
                    if (ks.IsKeyDown(Keys.S))
                    {
                        Movement.Y = _MovementSpeed * (Movement.Y + 1);
                        _Direction = direction.South;
                        movinganimationX = 0;
                    }


                    if (ks.IsKeyDown(Keys.D) && ks.IsKeyDown(Keys.A)) { Movement.X = 0; movinganimationX = 0; }
                    if (ks.IsKeyDown(Keys.W) && ks.IsKeyDown(Keys.S)) { Movement.Y = 0; movinganimationX = 0; }

                    if (ks.IsKeyDown(Keys.I))
                    {
                        projectile_Movement.Y = _MovementSpeed * (Projectile_Movement.Y - 1);       
                        movinganimationX = 3;
                    }
                    if (ks.IsKeyDown(Keys.K))
                    {                        
                        projectile_Movement.Y = _MovementSpeed * (Projectile_Movement.Y + 1);
                        movinganimationX = 0;
                    }
                    if (ks.IsKeyDown(Keys.J))
                    {
                        projectile_Movement.X = _MovementSpeed * (Projectile_Movement.X - 1);
                        movinganimationX = 1;
                    }
                    if (ks.IsKeyDown(Keys.L))
                    {
                        projectile_Movement.X = _MovementSpeed * (Projectile_Movement.X + 1);                       
                        movinganimationX = 2;
                    }
                    Canshoot = true;

                    if (ks.IsKeyDown(Keys.I) && ks.IsKeyDown(Keys.K)) { Canshoot = false; }
                    if (ks.IsKeyDown(Keys.J) && ks.IsKeyDown(Keys.L)) { Canshoot = false; }

                    if (ks.IsKeyDown(Keys.Space) && Canshoot == true)
                    {
                        Timewaited++;

                        if (Timewaited > Timetowait / shootingspeed)
                        {
                            bool stationary = false;
                            if (Projectile_Movement == new Vector2(0, 0))
                            {
                                stationary = true;
                            }

                            projectiles.addprojectiles(content, Theplayer, Projectile_Movement, stationary);

                            Timewaited = 0;
                        }
                    }

                    _Location = _Location + Movement;

                    
                    if (Math.Abs(WindowWidthdiv3 - _Location.X) < 150 && Math.Abs(0 - _Location.Y) < 30)
                    {
                        MoveroomAnimation();
                    }

                    if (Math.Abs(WindowWidthdiv3 - _Location.X) < 100 && Math.Abs(WindowHeight - _Location.Y) < 10)
                    {

                        MoveroomAnimation();
                    }

                    if (Math.Abs(0 - _Location.X) < 30 && Math.Abs(WindowHeightdiv2 - _Location.Y) < 150)
                    {

                        MoveroomAnimation();
                    }

                    if (Math.Abs(2 * WindowWidthdiv3 - _Location.X) < 30 && Math.Abs(WindowHeightdiv2 - _Location.Y) < 150)
                    {

                        MoveroomAnimation();
                    }
                }
            }
            else
            {
                Movement.X = -Movement.X;
                Movement.Y = -Movement.Y;
                _Location = _Location + Movement;
                
            }


            if (Theplayer._HitBox.Intersects(ThisRoom._Borders[0]))
            {
                Theplayer.Entered_Combat_Room = true;
            }
           
        }        

        public void movetocentre()
        {
            
        }

        public void MoveroomAnimation()
        {            
            switch (Direction) //redo using hitboxes 
            {
                case direction.North:
                    Movement.Y = _MovementSpeed * (Movement.Y - 1);
                    _Location = _Location + Movement;
                    if (Math.Abs(WindowWidthdiv3 - _Location.X) < 150 && Math.Abs(0 - _Location.Y) < 10)
                    {
                        Roomchange.Y = WindowHeight;
                        _Location = _Location + Roomchange;
                        Positioninmaze.Y = Positioninmaze.Y - 1;
                    }
                    Movingroom = true;
                    break;
                case direction.West:
                    Movement.X = _MovementSpeed * (Movement.X - 1);
                    _Location = _Location + Movement;
                    if (Math.Abs(0 - _Location.X) < 10 && Math.Abs(WindowHeightdiv2 - _Location.Y) < 100)
                    {
                        Positioninmaze.X = Positioninmaze.X - 1;
                        Roomchange.X = 2* WindowWidthdiv3;
                        _Location = _Location + Roomchange;
                    }
                    Movingroom = true;
                    break;
                case direction.South:
                    Movement.Y = _MovementSpeed * (Movement.Y + 1);
                    _Location = _Location + Movement;
                    if (Math.Abs(WindowWidthdiv3 - _Location.X) < 150 && Math.Abs(WindowHeight - _Location.Y) < 100)
                    {
                        Positioninmaze.Y = Positioninmaze.Y + 1;
                        Roomchange.Y = -WindowHeight;
                        _Location = _Location + Roomchange;
                    }
                    Movingroom = true;
                    break;
                case direction.East:
                    Movement.X = _MovementSpeed * (Movement.X + 1);
                    _Location = _Location + Movement;
                    if (Math.Abs(2* WindowWidthdiv3 - _Location.X) < 10 && Math.Abs(WindowHeightdiv2 - _Location.Y) < 100)
                    {
                        Positioninmaze.X = Positioninmaze.X + 1;
                        Roomchange.X = -2* WindowWidthdiv3;
                        _Location = _Location + Roomchange;
                    }
                    Movingroom = true;
                    break;
            }
            Movement = ResetVector;
            Roomchange = ResetVector;
            
            if ((Math.Abs(WindowWidthdiv3 - _Location.X) < WindowWidth / 5 && Math.Abs(0 - _Location.Y) < WindowHeight / 10) || (Math.Abs(0 - _Location.X) < WindowWidth / 10 && Math.Abs(WindowHeightdiv2 - _Location.Y) < WindowHeight / 7) || (Math.Abs(WindowWidthdiv3 - _Location.X) < WindowWidth / 6 && Math.Abs(WindowHeight - _Location.Y) < WindowHeight / 10) || (Math.Abs(WindowWidth / 1.5 - _Location.X) < WindowWidth / 6 && Math.Abs(WindowHeightdiv2 - _Location.Y) < WindowHeight / 5))
            {
                MovedRoom = true;
                Movingroom = false;                
            }
        }

        public void collisions(Enemies enemies,Timer timer)
        {
            bool remove = false;
            foreach (Enemy enemy in enemies._Enemies)
            {
                if (_HitBox.Intersects(enemy._HitBox))
                {
                    lives = lives - 1;
                    _Location = new Vector2(WindowWidthdiv3, WindowHeight / 2);
                    timer.PlayerLifeLost();
                    remove = true;
                }
            }

            if (remove == true)
            {
                enemies.RemoveEnemies();                
            }
            
        }

        

        public override void loadcontent(ContentManager content, string name)
        {
            _Texture = content.Load<Texture2D>(@name);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_Texture, _Location, new Rectangle(32 * movinganimationX, 0, 32, 32), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
        }


    }
}
