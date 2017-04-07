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
    public class BasicModelObject : GameObject
    {
        private string _asset;

        public BasicModelObject(string asset, Vector3 location)
            :base(location)
        {
            _asset = asset;
        }

        public override void Initialize()
        {
            Manager.AddComponent(new BasicEffectModel(_asset));
            Manager.AddComponent(new BoxBody(10));

            base.Initialize();
        }

    }
}
