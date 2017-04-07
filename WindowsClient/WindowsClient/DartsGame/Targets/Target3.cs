using Engine.Base;
using Engine.Components.Graphics;
using Engine.Components.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsClient.DartsGame.Targets
{
    public class Target3 : GameObject
    {
        public Target3(Vector3 location)
            : base(location)
        {

        }
        public override void Initialize()
        {
            //exact same as target.cs except for differing controllers
            Manager.AddComponent(new BasicEffectModel("Board"));
            Manager.AddComponent(new BoxBody(0));
            Manager.AddComponent(new TargetController3(1));

            base.Initialize();
        }
    }
}
