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
  public class DartsMenuScene : Scene
    {
        //scene that contains the intial menu.
        SpriteBatch batch;//used to draw
        Texture2D menu;//the actual texture used to be the context for the menu
        Song Music;//the music that will play as the backing them when the spacebar is pressed
        public DartsMenuScene(GameEngine engine)
            :base("menu", engine)
        {

        }

        //create a new instance of SpriteBatch
        //load in the menu texture
        public override void Initialize()
        {
            Music = GameUtilities.Content.Load<Song>("Music\\TavernMusic");//matching the variables to their respected positions.
            batch = new SpriteBatch(GameUtilities.GraphicsDevice);
            menu = GameUtilities.Content.Load<Texture2D>("Textures\\DARTMENU");

            base.Initialize();
        }

        //if the spacebar is pressed
        //Use Engine.LoadScene to load the BowlingScene
        public override void HandleInput()
        {
            if (InputEngine.IsKeyPressed(Keys.Space))//load the level scene when pressed
            {
                Engine.LoadScene(new DartScene(Engine));
                MediaPlayer.Play(Music);//plays the backing theme , bonus points if you recognise it
            }

            base.HandleInput();
        }

        //Using SpriteBatch draw the menu texture
        //the texture should fill the entire window
        public override void DrawUI()
        {
            batch.Begin();

            batch.Draw(menu,
                new Rectangle(0, 0,
                GameUtilities.GraphicsDevice.Viewport.Width,
                GameUtilities.GraphicsDevice.Viewport.Height),
                Color.White);//draws the texture used for the background in its designated position

            batch.End();

            base.DrawUI();
        }
    }
}
