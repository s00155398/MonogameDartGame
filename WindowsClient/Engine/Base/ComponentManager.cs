using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Engine.Base
{
    public class ComponentManager
    {
        private List<Component> components = new List<Component>();
        public List<Component> Components { get { return components; } }

        private List<string> awaitingRemoval = new List<string>();

        public GameObject Owner { get; set; }
        private bool isInitialized = false;
        public bool IsInitialized { get { return isInitialized; } }

        public ComponentManager(GameObject owner)
        {
            Owner = owner;
        }

        public void Initialize()
        {
            foreach (var c in components)
                c.Initialize();

            isInitialized = true;
        }

        public void Update()
        {
            foreach (var comp in components)
                if (comp.Enabled)
                    comp.Update();

            foreach (string id in awaitingRemoval)
                RemoveComponent(id);
        }

        public void Draw(CameraComponent camera)
        {
            foreach (var rcomp in components.OfType<RenderComponent>())
                if (rcomp.Enabled)
                    rcomp.Draw(camera);
        }

        public bool HasComponent<T>()
        {
            return components.Any(c => c.GetType() == typeof(T) 
            || c.GetType().IsSubclassOf(typeof(T)));
        }

        public void AddComponent(Component component)
        {
            component.Manager = this;

            if (isInitialized)
                component.Initialize();

            component.OnDestroy += Component_OnDestroy;

            components.Add(component);
        }

        private void Component_OnDestroy(string id)
        {
            awaitingRemoval.Add(id);
        }

        public void RemoveComponent(Component component)
        {
            components.RemoveAt(components.IndexOf(component));
        }

        public void RemoveComponent(string id)
        {
            try
            {
                components.RemoveAt(components.IndexOf(components.First(c => c.ID == id)));
            }
            catch
            {

            }
        }

        public void RemoveComponent(int index)
        {
            if (index < components.Count && index > -1)
                components.RemoveAt(index);
        }

        public Component GetComponent(string id)
        {
            return components.FirstOrDefault(c => c.ID == id);
        }

        public Component GetComponent(Type componentType)
        {
            return components.FirstOrDefault(c => c.GetType() == componentType ||
            c.GetType().IsSubclassOf(componentType));
        }

        public void DestroyAll()
        {
          for(int i =0;i < components.Count;i++)
            {

            }
        }
    }
}
