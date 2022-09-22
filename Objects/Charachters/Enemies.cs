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
    class Enemies
    {
        public int Timetowait;
        public double Timewaited;
        public bool Returnedmultiplier = false;
        public double multiplier;
        List<Enemy> enemies = new List<Enemy>();
       

        public Enemies()
        {
            Timetowait = 50;
        }

        internal List<Enemy> _Enemies { get => enemies; set => enemies = value; }

        public void addenemy(int floor,ContentManager content,GameTime gameTime)
        {

            if(Returnedmultiplier == false)
            {
                multiplier = returningmultiplier(floor);
            }

            Timewaited++;

            if(Timewaited > (Timetowait*multiplier))
            {
                Enemy newenemy = new Enemy(floor,multiplier);
                newenemy.loadcontent(content, "enemy");
                _Enemies.Add(newenemy);
                Timewaited = 0;
            }       
        }

        public void RemoveEnemies()
        {
            enemies.Clear();
        }

        public double returningmultiplier(int floor)
        {
            double[] multipliers = new double[] {0, 0.9,0.96,0.93,0.8,0.75,0.74,0.7,0.67,0.6,0.53};

            return multipliers[floor];
        }
    }
}
