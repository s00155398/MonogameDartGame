using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;


namespace PipelineExtensions
{
    [ContentProcessor(DisplayName = "Waypoint Processor")]
    public class WaypointProcessor : ModelProcessor
    {
        private bool _preserveHeight;

        [DisplayName("Preserve Point Y Value")]
        [DefaultValue(true)]
        [Description("Saves the Original Y Value of each Point")]
        public bool PreservePointHeight
        {
            get { return _preserveHeight; }
            set { _preserveHeight = value; }
        }

        public override ModelContent Process(NodeContent input, ContentProcessorContext context)
        {
            ModelContent model = base.Process(input, context);

            //results will be stored in this collection
            List<Vector3> points = new List<Vector3>();

            //loop through each mesh ant the center of each of its bounding spheres
            foreach (ModelMeshContent mesh in model.Meshes)
            {
                //we will need to transform the center by the meshes parent bone matrix
                //if we dont they will all at the same position
                Matrix transform;

                if (mesh.ParentBone.Transform != null)
                    transform = mesh.ParentBone.Transform;
                else
                    transform = Matrix.Identity;

                var p = Vector3.Transform(mesh.BoundingSphere.Center, mesh.ParentBone.Transform);

                //using the property above we can make decisons
                if (PreservePointHeight)
                    points.Add(p);
                else
                    points.Add(new Vector3(p.X, 0, p.Z));
            }
            //we alwyas store the additonal data in the Tag property of the object
            model.Tag = points;
            return model;
        }



    }
}
