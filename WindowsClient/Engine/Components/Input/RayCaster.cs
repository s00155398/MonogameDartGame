using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Engine.Engines;
using Engine.Components.Physics;
using Engine.Base;

namespace Engine.Components.Input
{
    public delegate void GameObjectInfoDelegate
        (PhysicsComponent.GameObjectInfo info);

    public class RayCaster : Component
    {
        public event GameObjectInfoDelegate GameObjectSelected;

        public RayCaster() : base() { }

        public override void Update()
        {
            if(InputEngine.IsMouseLeftClick())
            {
                PhysicsComponent.GameObjectInfo result;

                PhysicsEngine.CastRay(
                    GameUtilities.CreateRayFromVector2(InputEngine.MousePosition),
                    1000,
                    true,
                    out result);

                if(result != null)
                {
                    if (GameObjectSelected != null)
                        GameObjectSelected(result);
                }
            }

            base.Update();
        }
    }
}
