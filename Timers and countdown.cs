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
        private int timeinRoom;
        private bool spawning = false;       
        private bool checkenemies = false;
        

        public bool Spawning { get => spawning; set => spawning = value; }
        public int Deadbuffer { get => deadbuffer; set => deadbuffer = value; }        
        public bool Checkenemies { get => checkenemies; set => checkenemies = value; }
        public int TimeinRoom { get => timeinRoom; set => timeinRoom = value; }

        public Timer(int floor)
        {
            WindowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            WindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _Location = new Vector2(WindowWidth * 2 / 3, WindowHeight / 3);
            TimetoSurvive = 2000 + floor * 100;
            TimeinRoom = 0;
        }

        public void LoadContent(ContentManager Content)
        {

        }

        public void update(GameTime gameTime)
        {
            TimeinRoom++;
            if(TimeinRoom >= buffertime && Deadbuffer == 0)
            {
                if(TimetoSurvive > TimeinRoom)
                {
                    Spawning = true;
                }
                else
                {
                    Spawning = false;
                    checkenemies = true;
                }
            }
            else
            {
                if (deadbuffer != 0)
                {
                    deadbuffer--;
                }
            }

        }

        public void resettimer()
        {
            TimeinRoom = 0;
            buffertime = 500;
            spawning = false;
            checkenemies = false;
        }

        public void PlayerLifeLost()
        {
            Deadbuffer = 100;
        }


    }
}
