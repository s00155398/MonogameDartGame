using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Engine.Base;
using Engine.Components.Graphics;
using Microsoft.Xna.Framework;
using Engine.Components.Physics;

namespace WindowsClient.GameObjects
{
    public class StaticModelObject : GameObject
    {
        private string _asset;

        public StaticModelObject(string asset, Vector3 location)
            :base(location)
        {
            _asset = asset;
        }

        public override void Initialize()
        {
            Manager.AddComponent(new BasicEffectModel(_asset));
            Manager.AddComponent(new StaticMeshBody());

            base.Initialize();
        }

    }
}
