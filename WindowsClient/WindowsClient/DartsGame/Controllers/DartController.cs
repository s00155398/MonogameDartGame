using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using Engine;
using Engine.Base;
using Engine.Components.Physics;
using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsClient.DartsGame.Targets;

namespace WindowsClient.DartsGame
{
  public class DartController : Component
    {
        //class used to control the dart such as movement and launching aswell as recovering the dart

        public float ThrowPower { get; set; }

        private float MovementSpeed = 0.1f;
        private BoxBody  dartBody;
        private BEPUutilities.Vector3 impulse = new BEPUutilities.Vector3();
        public bool wasHit = false;
        private Vector3 startLocation;
        private Quaternion startRotation;
        private float distance = 4;
        private DartsPlayer data;
       
        //has the ball being thrown
        public static bool hasThrown = false;

        public DartController()
            : base(){ }

        public DartController(DartsPlayer data)
        {
            this.data = data;
        }

        public override void Initialize()
        {
           
            if (Manager.HasComponent<BoxBody>())
            {
                dartBody = Manager.GetComponent(typeof(BoxBody)) as BoxBody;
                dartBody.Entity.BecomeKinematic();
            }
            else
            {
                Destroy();
            }

            startLocation = Manager.Owner.Location;
            startRotation = Manager.Owner.Rotation;
            Reset();
            base.Initialize();
        }
        private void Reset()
        {
            hasThrown = false;//this method puts the dart back in its original position and orientation so that the player will be ready to launch it again.
            ThrowPower = 0;
            //resets its velocity and makes it kinetimatic
            dartBody.Entity.LinearVelocity = BEPUutilities.Vector3.Zero;
            dartBody.Entity.AngularVelocity = BEPUutilities.Vector3.Zero;
            dartBody.Entity.BecomeKinematic();

            dartBody.Entity.Position = MathConverter.Convert(startLocation);//resets the position
            dartBody.Entity.Orientation = MathConverter.Convert(startRotation);//resets the orientation
        }

       

        public override void Update()
        {
            //If the R key is pressed then call Reset
            //As long as the spacebar is held, increase ThrowPower by 1(max 10)
            #region Reset and Charge Shot
            if (InputEngine.IsKeyPressed(Keys.R))
                Reset();

            if (InputEngine.IsKeyHeld(Keys.Space))
            {
                if (ThrowPower < 10)
                    ThrowPower++;//increments the power the longer the spacebar is held
            }
            #endregion
           
            //move the ball left and right
            if (!hasThrown)//contains the code that moves the dart up down left and right
            {
                if (InputEngine.IsKeyHeld(Keys.A))
                {
                    if (Manager.Owner.Location.X > startLocation.X - distance)
                        dartBody.Entity.WorldTransform *=
                            MathConverter.Convert(Matrix.CreateTranslation(-MovementSpeed, 0, 0));
                }


                else if (InputEngine.IsKeyHeld(Keys.D))
                {
                    if (Manager.Owner.Location.X < startLocation.X + distance)
                        dartBody.Entity.WorldTransform *=
                            MathConverter.Convert(Matrix.CreateTranslation(MovementSpeed, 0, 0));
                }
               
               
                if (InputEngine.IsKeyHeld(Keys.W))
                {
                    if (Manager.Owner.Location.Z > startLocation.Y - distance)
                        dartBody.Entity.WorldTransform *=
                            MathConverter.Convert(Matrix.CreateTranslation(0, MovementSpeed, 0));
                }
                if (InputEngine.IsKeyHeld(Keys.S))
                {
                    if (Manager.Owner.Location.Z > startLocation.Y + distance)
                        dartBody.Entity.WorldTransform *=
                            MathConverter.Convert(Matrix.CreateTranslation(0, -MovementSpeed, 0));
                }
            }
            if (!hasThrown)
                if (InputEngine.IsKeyPressed(Keys.Enter))//launches the dart dependant of the accrued power from the spacebar being held
                {
                    if (dartBody != null)
                    {
                        dartBody.Entity.BecomeDynamic(dartBody.Mass);
                        impulse.Z = -(ThrowPower * 10);//sends it away from the screen at the force of the throw power
                        dartBody.Entity.LinearVelocity += impulse;

                        //resets the variables to allow more throws
                        ThrowPower = 0;
                        hasThrown = true;
                    }
                }

            base.Update();
        }

      


    }
}
