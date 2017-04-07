using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Engine.Base;

namespace Engine.Components.Input
{
    public class SelectionHandler : ScriptComponent
    {
        public SelectionHandler() : base() { }

        public override void Initialize()
        {
            if (Manager.HasComponent<RayCaster>())
            {
                (Manager.GetComponent(typeof(RayCaster)) as RayCaster)
                    .GameObjectSelected += OnSelection;
            }

            base.Initialize();
        }

        public virtual void OnSelection(PhysicsComponent.GameObjectInfo info)
        {
        }
    }
}
