using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using Engine.Base;
using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Components.Input
{
    public class PlayerBallController : Component
    {
        public float MovementSpeed { get; set; }
        private PhysicsComponent physics;
        public bool IsOnGround { get; set; }

        public event GameObjectInfoDelegate ObjectHit;

        public PlayerBallController(float speed)
            :base()
        {
            IsOnGround = true;
            MovementSpeed = speed;
        }

        public override void Initialize()
        {
            if(Manager.HasComponent<PhysicsComponent>())
            {
                physics = Manager.GetComponent(typeof(PhysicsComponent)) as PhysicsComponent;
                physics.Entity.LinearDamping = 0.2f;
                physics.Entity.AngularDamping = 0.2f;
                physics.Entity.CollisionInformation.Events.InitialCollisionDetected += Events_InitialCollisionDetected;
            }
            else
            {
                Destroy();
            }

            base.Initialize();
        }

        private void Events_InitialCollisionDetected(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
        {
            if(other is StaticMesh)
            {
                IsOnGround = true;
            }
            else
            {
                if(other.Tag != null)
                {
                    var info = other.Tag as PhysicsComponent.GameObjectInfo;

                    if (ObjectHit != null)
                        ObjectHit(info);
                }
            }
        }

        public override void Update()
        {
            if (InputEngine.IsKeyHeld(Keys.A))
            {
                physics.Entity.ApplyImpulse(
                    MathConverter.Convert(Manager.Owner.Location),
                    new BEPUutilities.Vector3(-MovementSpeed, 0, 0));
            }
            else if (InputEngine.IsKeyHeld(Keys.D))
            {
                physics.Entity.ApplyImpulse(
                    MathConverter.Convert(Manager.Owner.Location),
                    new BEPUutilities.Vector3(MovementSpeed, 0, 0));
            }

            if (InputEngine.IsKeyHeld(Keys.W))
            {
                physics.Entity.ApplyImpulse(
                    MathConverter.Convert(Manager.Owner.Location),
                    new BEPUutilities.Vector3(0, 0, -MovementSpeed));
            }
            else if (InputEngine.IsKeyHeld(Keys.S))
            {
                physics.Entity.ApplyImpulse(
                    MathConverter.Convert(Manager.Owner.Location),
                    new BEPUutilities.Vector3(0, 0, MovementSpeed));
            }

            if (InputEngine.IsKeyPressed(Keys.Space))
            {
                if (IsOnGround)
                {
                    physics.Entity.ApplyImpulse(
                        MathConverter.Convert(Manager.Owner.Location),
                        new BEPUutilities.Vector3(0, MovementSpeed * 25, 0));

                    IsOnGround = false;
                }
            }

            base.Update();
        }
    }
}
