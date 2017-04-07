using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Base
{
    public class RenderComponent : Component
    {
        public RenderComponent() : base() { }
        public RenderComponent(string id) : base(id) { }

        public virtual void Draw(CameraComponent camera) { }
    }
}
