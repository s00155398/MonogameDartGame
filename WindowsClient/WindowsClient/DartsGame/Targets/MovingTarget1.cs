using Engine.Base;
using Engine.Components.Graphics;
using Engine.Components.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsClient.Scripts;
namespace WindowsClient.DartsGame
{
    public class MovingTarget1 : GameObject
    {
        
        public MovingTarget1(Vector3 location)
            : base(location)
        {
           
        }
        public override void Initialize()
        {
            
            Manager.AddComponent(new BasicEffectModel("Board"));
            Manager.AddComponent(new BobbingObject(5f));
            Manager.AddComponent(new BoxBody(0));//if removed the target will move but am stuck in regards on how to have collision happen  if no box body or body
            Manager.AddComponent(new MovingTargetController(1));
                             
          

            


            base.Initialize();
        }
       
    }
}
