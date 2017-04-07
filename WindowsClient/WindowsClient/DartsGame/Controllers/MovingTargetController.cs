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
using WindowsClient.DartsGame.Targets;

namespace WindowsClient.DartsGame
{
   public class MovingTargetController : Component
    {

        public float KillTime { get; set; }
    
        private BoxBody TargetBox;
        public bool wasHit = false;
        private float elapsed = 0;
        SoundEffect thud;
        public MovingTargetController(float killTime)
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
        private void CollidedWith(EntityCollidable sender, Collidable other, CollidablePairHandler pair)//compares the tags of collided objects and sets the bool to be true if the target touches the dart
        {
            if (other.Tag is PhysicsComponent.GameObjectInfo)
            {
                var tag = (other.Tag as PhysicsComponent.GameObjectInfo);

                if (tag.ObjectType == typeof(MovingTarget1) || tag.ObjectType == typeof(Dart))
                {
                    wasHit = true;
                  
                }
            }
        }
        public override void Update()
        {
           
            if (wasHit)//as a result of the target hitting the dart or vice versa the target is destroyed and the next target is spawned , also the sound effect for the hit is played
            {
                elapsed += GameUtilities.Time.ElapsedGameTime.Milliseconds;

                if (elapsed > KillTime)
                    Manager.Owner.Destroy(true);
                Manager.Owner.Scene.AddObject(
                   new Target2(new Vector3(-2, 8, -10)
                   ));
                DartsPlayer.TargetsHit++;
                thud.Play();
            }

            base.Update();
        }
    }

    }

