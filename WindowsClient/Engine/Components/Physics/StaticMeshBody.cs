using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Engine.Base;
using Microsoft.Xna.Framework;
using BEPUphysics.BroadPhaseEntries;
using Engine.Engines;

namespace Engine.Components.Physics
{
    public class StaticMeshBody : PhysicsComponent
    {
        private StaticMesh Mesh;

        public StaticMeshBody() : base(0) { }

        public override void Initialize()
        {
            var model = GetModelFromOwner();

            if(model != null)
            {
                Vector3[] vertices;
                int[] indices;

                ModelDataExtractor.GetVerticesAndIndicesFromModel(
                    model,
                    out vertices,
                    out indices);

                Mesh = new StaticMesh(
                    MathConverter.Convert(vertices),
                    indices,
                    new BEPUutilities.AffineTransform(
                        MathConverter.Convert(Manager.Owner.Location)));
                Mesh.Tag = Manager.Owner.ID;

                PhysicsEngine.AddStaticMesh(Mesh);
            }
        }

        public override void Destroy()
        {
            if (Mesh != null)
                PhysicsEngine.RemoveStaticMesh(Mesh);

            base.Destroy();
        }
    }
}
