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
    class Cursor : Charachter
    {
        
        private Vector2 mouseMoves;
        private Rectangle StartButton;
        private Rectangle CursorHitbox;
        private Rectangle LeaderboardsButton;
      
      

        public Cursor()
        {
            WindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            WindowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            StartButton = new Rectangle(780, 410, 310, 45);
            _Location = new Vector2(WindowWidth/ 2, WindowHeight / 2);
            _Size = 20;
        }

        public override void loadcontent(ContentManager content, string name)
        {
            _Texture = content.Load<Texture2D>(@"Mushroom Cursor");
        }

        public void update(GameTime gameTime,ref Game1.GameState gameState, ref bool newFloor)
        {
            MouseState MS = Mouse.GetState();
            
            mouseMoves.X = MS.X;
            mouseMoves.Y = MS.Y;
           
            _Location =  mouseMoves;
            CursorHitbox = new Rectangle((int)_Location.X, (int)_Location.Y, 80, 80);
            if (MS.LeftButton == ButtonState.Pressed)
            {
                if(CursorHitbox.Intersects(StartButton))
                {
                    gameState = Game1.GameState.Game;
                    newFloor = true;
                }
                    
            }
        }

        public void Draw(SpriteBatch spriteBatch,Background Title,Background Highlighted)
        {       

            MouseState MS = Mouse.GetState();

            mouseMoves.X = MS.X;
            mouseMoves.Y = MS.Y;

            

            if (CursorHitbox.Intersects(StartButton))
            {
                Highlighted.Draw(spriteBatch);
            }
            else
            {
                if (CursorHitbox.Intersects(LeaderboardsButton))
                {
                    Title.Draw(spriteBatch);
                }
                else
                {
                    Title.Draw(spriteBatch);
                }                
            }

            spriteBatch.Draw(_Texture, _Location, new Rectangle(0, 0, 100, 100), Color.White, 0, new Vector2(0, 0), 5, SpriteEffects.None, 0);
        }
    }
}
