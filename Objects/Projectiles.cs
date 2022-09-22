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
    class Projectiles
    {
        List<Projectile> projectiles = new List<Projectile>();

        public Projectiles()
        {

        }

        public List<Projectile> _Projectiles { get => projectiles; set => projectiles = value; }

        public void addprojectiles(ContentManager content, Player Theplayer, Vector2 Movement, bool stationary)
        {
            if (stationary == true)
            {
                Movement = new Vector2(0, 1 * Theplayer._MovementSpeed);
            }
            Projectile newprojectile = new Projectile(Theplayer, Movement);
            newprojectile.loadcontent(content, "Fireball");
            _Projectiles.Add(newprojectile);

        }
    }
}