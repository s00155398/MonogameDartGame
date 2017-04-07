using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Engine.Base;
using BEPUphysics.Entities.Prefabs;

namespace Engine.Components.Physics
{
    public class SphereBody : PhysicsComponent
    {
        public SphereBody() : base()
        { }

        public SphereBody(float mass) 
            :base(mass)
        {}

        public override void Initialize()
        {
            var model = GetModelFromOwner();
            var size = MeasureMesh(model);

            Entity = new Sphere(
                MathConverter.Convert(Manager.Owner.Location),
                size.X / 2,
                Mass);

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
