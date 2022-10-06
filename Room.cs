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
    class Room
    {
        
        private List<Rectangle> Border = new List<Rectangle>();
        private int windowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        private int windowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; 
        
        public List<Rectangle> _Borders { get => Border; set => Border = value; }

        public Room()
        {            


        }

        public void GenerateBorders(Maze floormaze, Player ThePlayer)
        {
            _Borders.Clear();
            _Borders.Add(new Rectangle(windowWidth / 12, windowHeight / 10, windowWidth / 2, windowHeight * 4 / 5));

            if (floormaze._Mazegenerated[(int)ThePlayer._Positioninmaze.X, (int)ThePlayer._Positioninmaze.Y] == 2)
            {
                _Borders.Add(new Rectangle(windowWidth / 4, 0, windowWidth / 6, windowHeight / 10));
                _Borders.Add(new Rectangle(windowWidth * 7 / 12, windowHeight * 2 / 5, windowWidth / 12, windowHeight / 5));
                _Borders.Add(new Rectangle(windowWidth / 4, windowHeight * 9 / 10, windowWidth / 6, windowHeight / 10));
                _Borders.Add(new Rectangle(0, windowHeight * 2 / 5, windowWidth / 12, windowHeight / 5));
                ThePlayer.Entered_Combat_Room = false;
            }
            else
            {
                if (floormaze._Mazegenerated[(int)ThePlayer._Positioninmaze.X, (int)ThePlayer._Positioninmaze.Y - 1] != 1)
                {
                    _Borders.Add(new Rectangle(windowWidth / 4, 0, windowWidth / 6, windowHeight / 10));
                }

                if (floormaze._Mazegenerated[(int)ThePlayer._Positioninmaze.X + 1, (int)ThePlayer._Positioninmaze.Y] != 1)
                {
                    _Borders.Add(new Rectangle(windowWidth * 7 / 12, windowHeight * 2 / 5, windowWidth / 12, windowHeight / 5));
                }

                if (floormaze._Mazegenerated[(int)ThePlayer._Positioninmaze.X, (int)ThePlayer._Positioninmaze.Y + 1] != 1)
                {
                    _Borders.Add(new Rectangle(windowWidth / 4, windowHeight * 9 / 10, windowWidth / 6, windowHeight / 10));
                }

                if (floormaze._Mazegenerated[(int)ThePlayer._Positioninmaze.X - 1, (int)ThePlayer._Positioninmaze.Y] != 1)
                {
                    _Borders.Add(new Rectangle(0, windowHeight * 2 / 5, windowWidth / 12, windowHeight / 5));
                }
            }




        }

      
    }
}
