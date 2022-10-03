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
        private float TimetoSurvive;
        private int buffertime = 500;
        private int deadbuffer=0;
        private int timeinRoom = 1;
        private bool spawning = false;
        private bool roomfinished = true;
        private bool checkenemies = false;
        

        public bool Spawning { get => spawning; set => spawning = value; }
        public bool Roomfinished { get => roomfinished; set => roomfinished = value; }
        public int Deadbuffer { get => deadbuffer; set => deadbuffer = value; }
        public int TimeinRoom { get => TimeinRoom; set => TimeinRoom = value; }
        public bool Checkenemies { get => checkenemies; set => checkenemies = value; }

        public Timer(int floor)
        {
            WindowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            WindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _Location = new Vector2(WindowWidth * 2 / 3, WindowHeight / 3);
            TimetoSurvive = 200 + floor * 100;
           
        }

        public void LoadContent(ContentManager Content)
        {

        }

        public void update(GameTime gameTime)
        {
            timeinRoom++;
            if(timeinRoom >= buffertime && Deadbuffer == 0)
            {
                if(TimetoSurvive > timeinRoom)
                {
                    Spawning = true;
                }
                else
                {
                    Spawning = false;
                    
                }
            }
            else
            {
                Deadbuffer--;
            }

        }

        public void resettimer()
        {
            timeinRoom = 0;
            buffertime = 500;
        }

        public void PlayerLifeLost()
        {
            Deadbuffer = 100;
        }


    }
}

