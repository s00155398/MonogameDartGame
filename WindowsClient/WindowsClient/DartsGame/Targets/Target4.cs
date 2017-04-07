using Engine.Base;
using Engine.Components.Graphics;
using Engine.Components.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsClient.DartsGame
{
   public class Target4 : GameObject
    {
        public Target4(Vector3 location) 
            : base(location)
        {

        }
        public override void Initialize()
        {
            Manager.AddComponent(new BasicEffectModel("Board"));//loading of model used to display
            Manager.AddComponent(new BoxBody(0));//body of the target used to detect collision and set mass
            Manager.AddComponent(new TargetController4(1));//link to controller for this object, 

            base.Initialize();
        }
    }
}
