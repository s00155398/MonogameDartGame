using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Engine.Base;
using BEPUphysics.Entities.Prefabs;

namespace Engine.Components.Physics
{
    public class CylinderBody : PhysicsComponent
    {
        public CylinderBody() : base()
        { }

        public CylinderBody(float mass) 
            :base(mass)
        {}

        public override void Initialize()
        {
            var model = GetModelFromOwner();
            var size = MeasureMesh(model);

            Entity = new Cylinder(
                MathConverter.Convert(Manager.Owner.Location),
                size.Y,
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
