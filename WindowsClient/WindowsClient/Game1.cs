using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Engine;
using Engine.Engines;
using Microsoft.Xna.Framework.Media;

namespace WindowsClient
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        GameEngine engine;
       
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferMultiSampling = true;
            graphics.ApplyChanges();

            IsMouseVisible = true;
            IsFixedTimeStep = true;
            Content.RootDirectory = "Content";

            TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 16);
            graphics.SynchronizeWithVerticalRetrace = true;

            engine = new GameEngine(this);
        }

        protected override void Initialize()
        {
            GameUtilities.GraphicsDevice = GraphicsDevice;
            GameUtilities.Content = Content;
            GameUtilities.DebugTextColor = Color.White;
            GameUtilities.Random = new System.Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            GameUtilities.DebugSpriteBatch = new SpriteBatch(GraphicsDevice);
            engine.LoadScene(new DartsGame.DartsMenuScene(engine));
           
        }

        protected override void Update(GameTime gameTime)
        {
            GameUtilities.GameHasFocus = this.IsActive;
            GameUtilities.Time = gameTime;

            if (InputEngine.IsKeyPressed(Keys.Escape))
                Exit();

            if (InputEngine.IsKeyPressed(Keys.F1))
                graphics.ToggleFullScreen();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
