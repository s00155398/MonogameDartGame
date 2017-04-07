using Engine;
using Engine.Base;
using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsClient.DartsGame
{
  public  class DartsRetryScreen : Scene
    {
        //scene similar to the intial menu scene just with different texture to be the context.
        SpriteBatch batch;
        Texture2D menu;
        Song Music;
        public DartsRetryScreen(GameEngine engine)
            :base("menu", engine)
        {

        }

        //create a new instance of SpriteBatch
        //load in the menu texture
        public override void Initialize()
        {
            Music = GameUtilities.Content.Load<Song>("Music\\TavernMusic");
            batch = new SpriteBatch(GameUtilities.GraphicsDevice);
            menu = GameUtilities.Content.Load<Texture2D>("Textures\\FailScreen");

            base.Initialize();
        }

        //if the spacebar is pressed
        //Use Engine.LoadScene to load the BowlingScene
        public override void HandleInput()
        {
            if (InputEngine.IsKeyPressed(Keys.Space))//starts the game again if pressed aswell as restarting the music and resetting all the core variables of the game
            {
                Engine.LoadScene(new DartScene(Engine));
                MediaPlayer.Play(Music);
                DartsPlayer.TargetsHit = 0;
                TargetController4.win = false;
                DartScene.dartsLeft = 10;
            }

            base.HandleInput();
        }

        //Using SpriteBatch draw the menu texture
        //the texture should fill the entire window
        public override void DrawUI()
        {
            batch.Begin();

            batch.Draw(menu,//draws the texture for the menu context
                new Rectangle(0, 0,
                GameUtilities.GraphicsDevice.Viewport.Width,
                GameUtilities.GraphicsDevice.Viewport.Height),
                Color.White);

            batch.End();

            base.DrawUI();
        }
    }
}
