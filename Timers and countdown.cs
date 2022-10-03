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
    class Timer:Charachter
    {
        public float TimetoSurvive;
        public int buffertime = 10;
        public int TimeInRoom;
        public int deadbuffer=0;
        private bool spawning = false;
        private bool roomfinished = true;

        public bool Spawning { get => spawning; set => spawning = value; }
        public bool Roomfinished { get => roomfinished; set => roomfinished = value; }

        public Timer(int floor)
        {
            WindowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            WindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _Location = new Vector2(WindowWidth * 2 / 3, WindowHeight / 3);

            TimetoSurvive = 20 + floor * 10;
        }

        public void LoadContent(ContentManager Content)
        {

        }

        public void update(GameTime gameTime)
        {
            TimeInRoom++;
            if(TimeInRoom > buffertime && deadbuffer == 0)
            {
                Spawning = true;
            }

        }

        public void resettimer()
        {
            TimeInRoom = 0;
            buffertime = 10;
        }

        public void PlayerLifeLost()
        {
            TimeInRoom = TimeInRoom - 15;
            deadbuffer = 5;
        }
    }
}
