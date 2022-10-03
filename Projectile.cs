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
    class Projectile : Charachter
    {
        private int windowWidth;
        private int windowHeight;
        private direction direction;
        private Vector2 movement;       
        private bool isremoved;
        private int damage;
        private Keys[] inputs = new Keys[100];


        public direction Direction { get => direction; set => direction = value; }
        public Vector2 Movement { get => movement; set => movement = value; }
        public bool Isremoved { get => isremoved; set => isremoved = value; }
       

        public Projectile(Player player, Vector2 Movement)
        {
            Vector2 positionsetter = new Vector2(player._Location.X + 16, player._Location.Y + 16);
            windowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            windowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _Location = positionsetter;
            movement = Movement;
            damage = 50;
        }

        public override void loadcontent(ContentManager content, string name)
        {
            _Texture = content.Load<Texture2D>(@name);
        }

        public void update(GameTime gameTime)
        {
            _HitBox = new Rectangle((int)_Location.X, (int)_Location.Y, 16, 16);
            _Location = _Location + movement * 5;
        }

        public void Collisions(Enemies enemies)
        {
            foreach (Enemy enemy in enemies._Enemies)
            {
                if (_HitBox.Intersects(enemy._HitBox))
                {
                    enemy.Health = enemy.Health - damage;
                }
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if ((_Location.X < 2*windowWidth/3 && _Location.X > 0) && (_Location.Y < windowHeight && _Location.Y > 0))
            {
                spriteBatch.Draw(_Texture, _Location, new Rectangle(0, 0, 100, 100), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
            else
            {
                Isremoved = true;
            }

        }


    }
}