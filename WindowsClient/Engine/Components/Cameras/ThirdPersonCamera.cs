using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Engines;
using Engine.Base;

namespace Engine.Components.Cameras
{
    public class ThirdPersonCamera : CameraComponent
    {
        public Vector3 Offset { get; set; }

        public ThirdPersonCamera(Vector3 direction, Vector3 offset) : base()
        {
            CameraDirection = direction;
            Enabled = true;
            Offset = offset;
        }

        public override void Initialize()
        {
            NearPlane = 1.0f;
            FarPlane = 10000.0f;
            UpVector = Vector3.Up;
            CameraDirection.Normalize();

            Update();

            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.AspectRatio,
                NearPlane,
                FarPlane);

            base.Initialize();
        }

        public override void Update()
        {
            CurrentTarget = (Manager.Owner.Location) + CameraDirection;

            View = Matrix.CreateLookAt(
               Manager.Owner.Location + Offset,
                CurrentTarget,
                UpVector);

            base.Update();
        }
    }
}
