using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace Engine.Base
{
    public class GameObject
    {
        public Scene Scene { get; set; }

        public string ID { get; set; }
        public ComponentManager Manager { get; set; }
        public bool Enabled { get; set; }

        public event ObjectStringIDHandler OnDestroy;

        public Matrix World { get; set; }
        public Vector3 Location { get { return World.Translation; } }
        public Vector3 Scale { get { return World.Scale; } }
        public Quaternion Rotation { get { return World.Rotation; } }

        public GameObject Parent { get; set; }
        public List<GameObject> Children { get; set; }

        public GameObject() {

            ID = this.GetType().Name + Guid.NewGuid();

            Manager = new ComponentManager(this);
            Enabled = true;
            World = Matrix.Identity;
            Children = new List<GameObject>();
        }

        public GameObject(Vector3 location)
        {
            ID = this.GetType().Name + Guid.NewGuid();

            Manager = new ComponentManager(this);
            Enabled = true;
            World = Matrix.Identity * Matrix.CreateTranslation(location);
            Children = new List<GameObject>();
        }

        public virtual void Initialize()
        {
            Manager.Initialize();
        }

        public virtual void Update()
        {
            if(Enabled)
            {
                if (Parent != null)
                    World += Parent.World;

                Manager.Update();
            }
        }

        public void AttachGameObjectTo(ref GameObject objectToAttach)
        {
            if(objectToAttach != null)
            {
                objectToAttach.Parent = this;
                Children.Add(objectToAttach);
            }
        }

        public void DetachFromParent()
        {
            if (Parent != null)
                Parent = null;
        }

        public float GetDistanceTo(GameObject otherObject)
        {
            return Vector3.Distance(this.Location, otherObject.Location);
        }

        public void Draw(CameraComponent camera)
        {
            Manager.Draw(camera);
        }

        public void Destroy(bool shouldDestroyChildren)
        {
            if (shouldDestroyChildren)
                foreach (var c in Children)
                    c.Destroy(true);
            else
                foreach (var c in Children)
                    c.DetachFromParent();

            Children.Clear();

            //GameObject Destroy Method
            Manager.Components.ForEach(c => c.Destroy());

            if (OnDestroy != null)
                OnDestroy(ID);
        }
    }
}
