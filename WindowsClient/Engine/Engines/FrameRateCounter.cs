using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Engine.Engines
{
    public class FrameRateCounter : DrawableGameComponent
    {
        //frame count from last second
        static int frameRate = 0;
        public static int FrameRate { get { return frameRate; } }

        //current frames being counted
        int frameRateCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;

        public FrameRateCounter(Game game)
            :base(game)
        {
            game.Components.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if(elapsedTime > TimeSpan.FromSeconds(1))
            {
                //after 1 second get the total number frames drawn
                frameRate = frameRateCounter;
                frameRateCounter = 0;
                elapsedTime -= TimeSpan.FromSeconds(1);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            frameRateCounter++;

            base.Draw(gameTime);
        }
    }
}
