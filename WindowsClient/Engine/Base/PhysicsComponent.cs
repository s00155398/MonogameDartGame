
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BEPUphysics.Entities;
using Microsoft.Xna.Framework.Graphics;
using Engine.Components.Graphics;
using Engine.Engines;
using Microsoft.Xna.Framework;

namespace Engine.Base
{
    public class PhysicsComponent : Component
    {
        public Entity Entity { get; set; }
        public float Mass { get; set; }

        public PhysicsComponent()
            : base()
        {

        }

        public PhysicsComponent(float mass)
            : base()
        {
            Mass = mass;
        }

        public override void Update()
        {
            if (Entity != null)
                Manager.Owner.World = 
                    MathConverter.Convert(Entity.WorldTransform);
     
            base.Update();
        }

        public Model GetModelFromOwner()
        {
            if(Manager.HasComponent<BasicEffectModel>())
            {
                var bmc = (BasicEffectModel)Manager.GetComponent(
                    typeof(BasicEffectModel));

                if (bmc.Model != null)
                    return bmc.Model;
                else return null;
            }
            else
            {
                return null;
            }
        }

        public Vector3 MeasureMesh(Model meshToMeasure)
        {
            Vector3[] vertices;
            int[] indices;

            ModelDataExtractor.GetVerticesAndIndicesFromModel(
                meshToMeasure,
                out vertices,
                out indices);

            var box = BoundingBox.CreateFromPoints(vertices);
            return box.Max - box.Min;//size of box
        }

        public class GameObjectInfo
        {
            public string ID { get; set; }
            public Type ObjectType { get; set; }
        }

        public override void Initialize()
        {
            if (Entity != null)
            {
                GameObjectInfo info = new GameObjectInfo()
                {
                    ID = Manager.Owner.ID,
                    ObjectType = Manager.Owner.GetType()
                };

                Entity.Tag = info;
                Entity.CollisionInformation.Tag = info;
            }

            if (Entity != null)
                PhysicsEngine.AddEntity(Entity);

            base.Initialize();
        }

        public override void Destroy()
        {
            if (Entity != null)
                PhysicsEngine.RemoveEntity(Entity);

            base.Destroy();
        }
    }
}
