using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace Computer_Science_end_project
{
    public class Game1 : Game
    {
        Enemies Theenemies;
        Player thePlayer;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Background BackgroundInTitle;
        Background BackgroundInTitleHighlighted;
        Background ThebackgroundInGame;
        Projectiles TheProjectiles;
        GameState gameState;
        Room TheRoom;
        Cursor TheCursor;
        Maze mazeforfloor;
        bool newFloor = true;
        RoomState roomState;
        Texture2D[] Textures = new Texture2D[50];
        SpriteFont GameFont;
        Timer CombatRoomTimer;
        bool IsDone = false;
        int floor = 1;
        int windowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int windowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public enum GameState
        {
            Menu,
            Game,
            Gameover,
            Leaderboards,
        }

        public enum RoomState
        {
            Empty,
            Combat,
            Shop,
            Cinematic,
            RoomChange,
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;   // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;   // set this value to the desired height of your window
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            roomState = RoomState.Empty;
            gameState = GameState.Menu;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ThebackgroundInGame = new Background(true);
            BackgroundInTitle = new Background(false);
            BackgroundInTitleHighlighted = new Background(false);
            thePlayer = new Player();
            TheProjectiles = new Projectiles();
            TheCursor = new Cursor();
            Theenemies = new Enemies();
            mazeforfloor = new Maze();
            TheRoom = new Room();
            CombatRoomTimer = new Timer(floor);
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.6
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Textures[0] = Content.Load<Texture2D>(@"Crossroads");
            thePlayer.loadcontent(Content, "Mushroom Man");
            BackgroundInTitle.LoadContent(Content, "Title Screen");
            BackgroundInTitleHighlighted.LoadContent(Content, "Highlighted Title Screen");
            TheCursor.loadcontent(Content, "Mushroom Cursor");
            mazeforfloor.Loadcontent(Content);
            GameFont = Content.Load<SpriteFont>(@"MyFont");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here#
            TheCursor.update(gameTime, ref gameState, ref newFloor);


            if (newFloor == true)
            {
                newFloor = false;
                ThebackgroundInGame.Texture = Textures[0]; //ThebackgroundInGame.TextureChecking(mazeforfloor, thePlayer)
                mazeforfloor.generatemaze(floor);
                TheRoom.GenerateBorders(mazeforfloor, thePlayer);
                floor++;
                roomState = RoomState.Empty;
                IsDone = false;
            }


            if (gameState == GameState.Game)
            {
                if (thePlayer.Movingroom == true) //changing the texture of background according to the rooms surrounding it
                {
                    ThebackgroundInGame.Texture = Textures[0]; //ThebackgroundInGame.TextureChecking(mazeforfloor, thePlayer)
                }

                if (thePlayer.MovedRoom1 == true)
                {
                    TheRoom.GenerateBorders(mazeforfloor, thePlayer);
                    thePlayer.MovedRoom1 = false;
                    Theenemies.RemoveEnemies();
                    mazeforfloor.RoomStateChecking(ref roomState, mazeforfloor, thePlayer);
                }

                PlayerUpdating(gameTime); //an update method called for just player to make code less scrambled

                ProjectileUpdating(gameTime);//an update method called for just projectiles to make code less scrambled

                if (roomState == RoomState.Combat && thePlayer.Entered_Combat_Room)
                {
                    if (!IsDone)
                    {
                        TheRoom._Borders.Clear();
                        TheRoom._Borders.Add(new Rectangle(windowWidth / 12, windowHeight / 10, windowWidth / 2, windowHeight * 4 / 5));
                        IsDone = true;
                    }
                    CombatRoomTimer.update(gameTime);
                   //checking if past the buffer phase for the room 
                    
                   EnemiesUpdating(gameTime); //an update method called for just enemies to make code less scrambled called when in a combat room    
                    

                   if (!Theenemies._Enemies.Any() && CombatRoomTimer.Checkenemies)
                   {
                       roomState = RoomState.Empty;
                       mazeforfloor._Mazegenerated[(int)thePlayer._Positioninmaze.X, (int)thePlayer._Positioninmaze.Y] = 0;
                       TheRoom.GenerateBorders(mazeforfloor, thePlayer);
                        CombatRoomTimer.resettimer();
                   }
                }
            }

            base.Update(gameTime);
        }

        public void ProjectileUpdating(GameTime gameTime)
        {
            foreach (Projectile projectile in TheProjectiles._Projectiles)
            {
                projectile.update(gameTime);
                projectile.Collisions(Theenemies);
            }

            TheProjectiles._Projectiles = TheProjectiles._Projectiles.Where(x => !x.Isremoved).ToList(); //removes projectiles which have hit an enemy
        }

        public void EnemiesUpdating(GameTime gameTime)
        {
            if (CombatRoomTimer.Spawning)
            {
                Theenemies.addenemy(floor, Content, gameTime);
            }

            foreach (Enemy enemy in Theenemies._Enemies)
            {
                enemy.update(gameTime, thePlayer);
            }
            Theenemies._Enemies = Theenemies._Enemies.Where(x => !x.IsRemoved).ToList();
        }

        public void PlayerUpdating(GameTime gameTime)
        {
            thePlayer.Update(gameTime, TheProjectiles, Content, thePlayer, TheRoom);
            thePlayer.collisions(Theenemies,CombatRoomTimer);

            if (thePlayer.lives == 0)
            {
                gameState = GameState.Gameover;
            }
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            // TODO: Add your drawing code here

            if (gameState == GameState.Menu)
            {
                BackgroundInTitle.Draw(spriteBatch);

                TheCursor.Draw(spriteBatch, BackgroundInTitle, BackgroundInTitleHighlighted);
            }
            else
            {
                if (gameState == GameState.Game)
                {

                    ThebackgroundInGame.Draw(spriteBatch);

                    mazeforfloor.draw(spriteBatch, thePlayer);
                    foreach (Projectile projectile in TheProjectiles._Projectiles)
                    {
                        projectile.Draw(spriteBatch);
                    }

                    if (roomState == RoomState.Combat)
                    {
                        foreach (Enemy enemy in Theenemies._Enemies)
                        {
                            enemy.Draw(spriteBatch);
                        }
                    }

                    thePlayer.Draw(spriteBatch);
                }
                else
                {
                    spriteBatch.DrawString(GameFont, "Game over", new Vector2(800, 500), Color.Red);
                }
            }

            spriteBatch.DrawString(GameFont, "Lives: " + thePlayer.lives, new Vector2(1300, 800), Color.Red);
            spriteBatch.DrawString(GameFont, "X " + thePlayer._Location.X + " Y " + thePlayer._Location.Y, new Vector2(1300, 900), Color.Red);
            spriteBatch.DrawString(GameFont, "X " + TheCursor._Location.X + " Y " + TheCursor._Location.Y, new Vector2(1300, 700), Color.Red);

            if (TheProjectiles._Projectiles.Any())
            {
                spriteBatch.DrawString(GameFont, "proj directions X:" + TheProjectiles._Projectiles[0].Movement.X + "Y: " + TheProjectiles._Projectiles[0].Movement.Y, new Vector2(1300, 750), Color.Red);
                
            }

            if (thePlayer.Canshoot)
            {
                spriteBatch.DrawString(GameFont,"true", new Vector2(1300, 850), Color.Red);
            }
            else
            {
                spriteBatch.DrawString(GameFont, "false", new Vector2(1300, 850), Color.Red);
            }

            spriteBatch.End();
        }
    }
}

