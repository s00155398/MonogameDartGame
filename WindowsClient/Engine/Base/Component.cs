using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Base
{
    public delegate void ObjectStringIDHandler(string id);

    public class Component
    {
        public string ID { get; set; }
        public bool Enabled { get; set; }

        public ComponentManager Manager { get; set; }
        public event ObjectStringIDHandler OnDestroy;
        public Component()
        {
            ID = this.GetType().Name + Guid.NewGuid();
            Enabled = true;
        }

        public Component(string id) { ID = id;  Enabled = true; }

        public virtual void Initialize() { }
        public virtual void Update() { }

        public virtual void Destroy()
        {
            if (OnDestroy != null)
                OnDestroy(ID);
        }
    }
}
