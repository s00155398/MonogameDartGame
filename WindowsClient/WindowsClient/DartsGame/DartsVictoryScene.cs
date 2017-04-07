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
using WindowsClient.DartsGame;

namespace WindowsClient.DartsGame
{
   public class DartsVictoryScene : Scene
    {
        //scene occurs only if the player wins the game
        //again similar to the menu scene and the retry screen
        SpriteBatch batch;
        Texture2D menu;
        Song Music;
        SpriteFont Font;
        int dartsRemaining;//variable to use to add to score for the end
        public DartsVictoryScene(GameEngine engine)
            :base("menu", engine)
        {

        }

        //create a new instance of SpriteBatch
        //load in the menu texture
        public override void Initialize()
        {
            Font = GameUtilities.Content.Load<SpriteFont>("Fonts\\bigFont");
            Music = GameUtilities.Content.Load<Song>("Music\\TavernMusic");
            batch = new SpriteBatch(GameUtilities.GraphicsDevice);
            menu = GameUtilities.Content.Load<Texture2D>("Textures\\Win");
            dartsRemaining = 10 - DartScene.dartsLeft;//to add to the score so that the player knows what he hit
            base.Initialize();
        }

        //if the spacebar is pressed
        //Use Engine.LoadScene to load the BowlingScene
        public override void HandleInput()
        {
            if (InputEngine.IsKeyPressed(Keys.Space))//resetting of variables again as in the retry screen if the player wishes to play again
            {
                Engine.LoadScene(new DartScene(Engine));
             
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

            batch.Draw(menu,
                new Rectangle(0, 0,
                GameUtilities.GraphicsDevice.Viewport.Width,
                GameUtilities.GraphicsDevice.Viewport.Height),
                Color.White);

            //draws the total score for the player to observe ie how many targets he got and how many darts he used to hit them.these are accessed by going into the player for the dart and pulling the targetsHit variable
            batch.DrawString(Font, "You hit" +" " + DartsPlayer.TargetsHit +" "+ "Targets with " + dartsRemaining + " " + "Darts",new Vector2(350, 250), Color.Black);
            batch.End();

            base.DrawUI();
        }
    }
}

