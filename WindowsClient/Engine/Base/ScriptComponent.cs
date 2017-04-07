using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Base
{
    public delegate void OnScriptComplete();

    public class ScriptComponent : Component
    {
        public event OnScriptComplete ScriptComplete;

        public ScriptComponent() : base() { }

        public virtual bool HasCompleted()
        {
            return false;
        }

        public override void Update()
        {
            if (HasCompleted())
                if (ScriptComplete != null)
                    ScriptComplete();

            base.Update();
        }

    }
}
