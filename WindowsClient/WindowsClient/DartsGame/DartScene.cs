
using Engine;
using Engine.Base;
using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsClient.GameObjects;

namespace WindowsClient.DartsGame
{
  public class DartScene : Scene
    {
        //variables used
        SpriteBatch batch;
        SpriteFont Font;
        Texture2D HUD;
        public static  int dartsLeft = 10;
        public DartsPlayer player;
        DartController dartController;
        SoundEffect fail;
       

        public DartScene(GameEngine engine)
            :base("Darts", engine)
        {

        }

        public override void Initialize()
        {
            //Assignment of Variables
            batch = new SpriteBatch(GameUtilities.GraphicsDevice);
            Font = GameUtilities.Content.Load<SpriteFont>("Fonts\\bigFont");
            fail = GameUtilities.Content.Load<SoundEffect>("SoundEffects\\Fail");
          
            AddObject(new StaticModelObject("room", Vector3.Zero));

            var target1 = new Target(new Vector3(1, 5, -3));//placement of 1st target
            AddObject(target1);
                
            var dart = new Dart(new Vector3(1, 6, 20));//placement of Dart
            AddObject(dart);
          
            player = new DartsPlayer(new Vector3(0, 5, 15));//placement of player/camera
            AddObject(player);

           

            HUD = GameUtilities.Content.Load<Texture2D>("Textures\\HUD");

            base.Initialize();

            dartController = dart.Manager.GetComponent(typeof(DartController)) as DartController;
        }
       
        public override void DrawUI()
        {
            batch.Begin();


            batch.Draw(HUD,
                new Rectangle(0, 0, GameUtilities.GraphicsDevice.Viewport.Width, GameUtilities.GraphicsDevice.Viewport.Height),
                Color.White);
            //drawing controls

            batch.DrawString(Font, "Space = Charge Shot", new Vector2(20, 90), Color.Red);
            batch.DrawString(Font, "Enter =  Shoot", new Vector2(20, 140), Color.Red);
            batch.DrawString(Font, "R =  New Dart", new Vector2(20, 300), Color.Red);
            //drawing of info to be tracked ie done by referencing the amount of targets hit from the dart class or the int variable of the remaining darts.
            batch.DrawString(Font, "Darts Remaining : " + dartsLeft, new Vector2(40, 10), Color.Black);
            batch.DrawString(Font, "Targets Hit : " + DartsPlayer.TargetsHit, new Vector2(600, 10), Color.Black);


            float  xLength = Font.MeasureString("Power: " + dartController.ThrowPower).X;//to record the numerical value of the strength of throwing the dart from its controller.

            batch.DrawString(//drawing the actual result of the power being recorded

                Font,
                "Power: " + dartController.ThrowPower,
                new Vector2(
                    GameUtilities.GraphicsDevice.Viewport.Width / 2 - xLength / 2,
                    GameUtilities.GraphicsDevice.Viewport.Height - 80), Color.Black);          
            batch.End();

            base.DrawUI();
        }

        

        public override void HandleInput()
        {
            //handles conditions to do with the game i.e when the last target is hit,if certain buttons are pressed and whether or not the last dart has been fired
            if (TargetController4.win == true)
            {
                Engine.LoadScene(new DartsVictoryScene(Engine));
            }
            if (InputEngine.IsKeyPressed(Keys.M))
            {                              
                Engine.LoadScene(new DartsMenuScene(Engine));               
            }
            if (DartController.hasThrown == true && InputEngine.IsKeyPressed(Keys.Enter))
            {
                dartsLeft--;
            }
            if (dartsLeft == -1)
            {
                Engine.LoadScene(new DartsRetryScreen(Engine));
                MediaPlayer.Stop();
                fail.Play();
            }
            base.HandleInput();
        }










    }
}
