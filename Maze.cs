using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;



namespace Computer_Science_end_project
{
    class Maze
    {
        private int[,] Mazegenerated;
        private bool[,] Visited;
        public Vector2 Location; 
        public Vector2 startlocation = new Vector2(1, 3);
        public Vector2 Position;
        public Vector2 newPosition;
        public Stack<Vector2> Positions = new Stack<Vector2>();
        public Vector2[] Neighbours = new Vector2[4];
        public Texture2D sprites;
        public Random RNG = new Random();   
        public int combatrooms;
        public Random Rng = new Random();

        public int[,] _Mazegenerated { get => Mazegenerated; set => Mazegenerated = value; }

        public Maze()
        {
            
            
        }

        public void Loadcontent(ContentManager content)
        {
            sprites = content.Load<Texture2D>(@"Minimap sprites");
            
        }

        public void FillMaze(int combatrooms) //filling in the rooms of the maxe
        {
            for (int u = 0; u < Mazegenerated.GetLength(1); u++)
            {
                for (int i = 0; i < Mazegenerated.GetLength(0); i++)
                {
                    if (u % 2 == 0) //even rows are always walls so fill with 1
                    {
                        Mazegenerated[i, u] = 1;
                        Visited[i, u] = true;
                    }
                    else
                    {
                        if (i % 2 == 0) // even columns are also always walls so fill with 1's
                        {
                            Mazegenerated[i, u] = 1;
                            Visited[i, u] = true;
                        }
                        else // only other things are going to be empty squares which can then be travelled between by the generation algorithm
                        {
                            Mazegenerated[i, u] = 0;
                            Visited[i, u] = false;

                            if (combatrooms>0 && Rng.Next(0, 3) == 2) // assigning a random chance to getting a combat room
                            {
                                Mazegenerated[i, u] = 2;
                                combatrooms--;
                            }
                        }
                    }
                  
                }
            }
            
        }

        public void generatemaze(int floor)
        {
            Mazegenerated = new int[2 * floor + 5, 7 ];
            Visited = new bool[2 * floor + 7, 7];
            int u = 1;
            combatrooms = 2 + floor*2;

            FillMaze(combatrooms/2);

            

            Position = startlocation;            
            Visited[(int)Position.X , (int)Position.Y ] = true;
            combatrooms = combatrooms / 2;

            do
            {
                
                int i = 0;
                checkneighbours(Position, ref i);
                if (i > 0)
                {
                    int Room = 0;
                    Positions.Push(Position);
                    newPosition = Neighbours[RNG.Next(0, i)];
                    Visited[(int)newPosition.X, (int)newPosition.Y] = true;

                    if (combatrooms > 0 && Rng.Next(0, 3) == 2)
                    {
                        combatrooms--;
                        Room = 2;
                    }

                    if (newPosition.X - Position.X == -2)
                    {
                        Mazegenerated[(int)newPosition.X + 1, (int)newPosition.Y] = Room;
                    }
                    if (newPosition.X - Position.X == 2)
                    {
                        Mazegenerated[(int)newPosition.X - 1, (int)newPosition.Y] = Room;
                    }
                    if (newPosition.Y - Position.Y == 2)
                    {
                        Mazegenerated[(int)newPosition.X, (int)newPosition.Y - 1] = Room;
                    }
                    if (newPosition.Y - Position.Y == -2)
                    {
                        Mazegenerated[(int)newPosition.X, (int)newPosition.Y + 1] = Room;
                    }

                    Position = newPosition;
                }
                else
                {
                    try
                    {
                        Position = Positions.Pop();
                        if (u > 0)
                        {
                            Mazegenerated[(int)newPosition.X, (int)newPosition.Y ] = 4;
                            u--;
                        }
                    }
                    catch 
                    {

                    }
                   
                }
            } while (Positions.Count > 0);

            Mazegenerated[1, 3] = 0;
            
        }

        public void checkneighbours(Vector2 Position,ref int i)
        {
            try
            {
                if (Visited[(int)Position.X- 2, (int)Position.Y] == false && Position.X - 2 > 0)
                {
                    Neighbours[i].X = Position.X - 2;
                    Neighbours[i].Y = Position.Y;
                    i++;
                }
            }
            catch (IndexOutOfRangeException)
            {

            }            

            try
            {
                if (Visited[(int)Position.X + 2 , (int)Position.Y ] == false && Position.X + 2 < Mazegenerated.GetLength(0))
                {
                    Neighbours[i].X = Position.X + 2;
                    Neighbours[i].Y = Position.Y;
                    i++;
                }
            }
            catch (IndexOutOfRangeException)
            {

            }           

            try
            {
                if (Visited[(int)Position.X, (int)Position.Y -2] == false && Position.Y -2 > 0)
                {
                    Neighbours[i].X = Position.X;
                    Neighbours[i].Y = Position.Y - 2;
                    i++;
                }
            }
            catch (IndexOutOfRangeException)
            {

            }           

            try
            {
                if (Visited[(int)Position.X, (int)Position.Y+2] == false && Position.Y +2 < Mazegenerated.GetLength(1))
                {
                    Neighbours[i].X = Position.X;
                    Neighbours[i].Y = Position.Y + 2;
                    i++;
                }
            }
            catch (IndexOutOfRangeException)
            {

            }
           
         
        }

        public void RoomStateChecking(ref Game1.RoomState roomState, Maze Maze, Player ThePlayer)
        {
            switch (Maze.Mazegenerated[(int)ThePlayer._Positioninmaze.X, (int)ThePlayer._Positioninmaze.Y])
            {
                case 0:
                    roomState = Game1.RoomState.Empty;
                    break;
                case 2:
                    roomState = Game1.RoomState.Combat;
                    break;
                case 3:
                    roomState = Game1.RoomState.Shop;
                    break;
                case 4:
                    roomState = Game1.RoomState.RoomChange;
                    break;
            }
        }

        public void draw(SpriteBatch spriteBatch,Player Theplayer)
        {
            for (int u = 0; u < Mazegenerated.GetLength(1); u++)
            {
                for (int i = 0; i < Mazegenerated.GetLength(0); i++)
                {
                    Location = new Vector2(1300 + 32 * i, u * 32);                    

                    switch (Mazegenerated[i, u])
                    {
                        case 0: //draw empty room
                            spriteBatch.Draw(sprites, Location, new Rectangle(64, 0, 32, 32), Color.White);
                            break;
                        case 1: //draw wall 
                            spriteBatch.Draw(sprites, Location, new Rectangle(32, 0, 32, 32), Color.White);
                            break;
                        case 2: // draw combat room
                            spriteBatch.Draw(sprites, Location, new Rectangle(96, 0, 32, 32), Color.White);
                            break;
                        case 3: //draw shop

                            break;
                        case 4: // draw floor change
                            
                            break;
                    }
                }
            }

            spriteBatch.Draw(sprites, new Vector2(1300+Theplayer._Positioninmaze.X*32, Theplayer._Positioninmaze.Y*32), new Rectangle(0, 0, 32, 32), Color.White);
        }
    }
}
