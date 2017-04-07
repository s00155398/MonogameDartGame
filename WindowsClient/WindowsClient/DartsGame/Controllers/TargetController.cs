using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using Engine;
using Engine.Base;
using Engine.Components.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsClient.DartsGame
{
  public  class TargetController : Component
    {
        //class exactly the same as the other targetcontrollers except for they have their own respective targets in the collsion detection
        public float KillTime { get; set; }
       
        private BoxBody TargetBox;
        public bool wasHit = false;        
        private float elapsed = 0;
        SoundEffect thud;
        public TargetController(float killTime)
            : base()
        {
            KillTime = killTime;
        }
        public override void Initialize()
        {
            thud = GameUtilities.Content.Load<SoundEffect>("SoundEffects\\DartThud");
            if (Manager.HasComponent<BoxBody>())
            {
                TargetBox = Manager.GetComponent(typeof(BoxBody)) as BoxBody;
                TargetBox.Entity.CollisionInformation.Events.DetectingInitialCollision += CollidedWith;
              
            }
            else
            {
                Destroy();
            }

            base.Initialize();
        }
        private void CollidedWith(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
        {
            if (other.Tag is PhysicsComponent.GameObjectInfo)
            {
                var tag = (other.Tag as PhysicsComponent.GameObjectInfo);

                if (tag.ObjectType == typeof(Target) || tag.ObjectType == typeof(Dart))
                {
                    wasHit = true;
                  
                }
            }
        }
        public override void Update()
        {
            if (wasHit)
            {
                elapsed += GameUtilities.Time.ElapsedGameTime.Milliseconds;

                if (elapsed > KillTime)
                    Manager.Owner.Destroy(true);
                Manager.Owner.Scene.AddObject(
                    new MovingTarget1(new Vector3(3,8,-20)
                    ));
                DartsPlayer.TargetsHit++;
                thud.Play();
            }

            base.Update();
        }

    }
}
