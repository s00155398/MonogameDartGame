using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Engine.Base;
using BEPUphysics.Entities.Prefabs;

namespace Engine.Components.Physics
{
    public class BoxBody : PhysicsComponent
    {
        public BoxBody() : base()
        { }

        public BoxBody(float mass) 
            :base(mass)
        {}

        public override void Initialize()
        {
            var model = GetModelFromOwner();
            var size = MeasureMesh(model);

            Entity = new Box(
                MathConverter.Convert(Manager.Owner.Location),
                size.X, //w
                size.Y, //h
                size.Z); //l

            if(Mass <= 0)
            {
                Entity.BecomeKinematic();
            }
            else
            {
                Entity.BecomeDynamic(Mass);
            }

            base.Initialize();
        }


    }
}
