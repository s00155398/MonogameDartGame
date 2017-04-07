using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Base
{
    public class Scene
    {
        public string ID { get; set; }
        public float KillHeight { get; set; } = -50;

        private List<GameObject> pool = new List<GameObject>();
        public List<GameObject> Pool { get { return pool; } }
        private List<string> objectsToBeDestroyed = new List<string>();

        protected GameEngine Engine;
        protected bool isInitialized = false;

        public Scene(string id, GameEngine engine)
        {
            ID = id;
            this.Engine = engine;
        }

        public void AddObject(GameObject newObject)
        {
            if(isInitialized)
            {
                newObject.Initialize();
            }
            newObject.OnDestroy += NewObject_OnDestroy;
            newObject.Scene = this;

            pool.Add(newObject);
        }

        public void Unload()
        {
            //mark every object for deletion
            foreach (var go in pool)
                go.Destroy(true);
        }

        private void NewObject_OnDestroy(string id)
        {
            objectsToBeDestroyed.Add(id);
        }

        public int GetObjectIndex(string id)
        {
            int index = -1;

            for (int i = 0; i < pool.Count; i++)
                if (pool[i].ID == id)
                    index = i;

            return index;
        }

        public GameObject GetObject(string id)
        {
            int objectIndex = GetObjectIndex(id);

            if (objectIndex > -1)
                return pool[objectIndex];
            else return null;
        }

        public void RemoveObject(string id)
        {
            int objectIndex = GetObjectIndex(id);

            if (objectIndex > -1)
                pool.RemoveAt(objectIndex);
        }

        public virtual void Initialize()
        {
            pool.ForEach(go => go.Initialize());
            isInitialized = true;
        }

        public virtual void Update()
        {
            for (int i = 0; i < pool.Count; i++)
                pool[i].Update();

            HandleInput();

            foreach (var id in objectsToBeDestroyed)
                RemoveObject(id);

            objectsToBeDestroyed.Clear();
        }

        public virtual void HandleInput() { }

        public void Draw3D(CameraComponent camera)
        {
            pool.ForEach(go => go.Draw(camera));
        }

        public bool HasObject(string id)
        {
            return pool.Any(go => go.ID == id);
        }

        public virtual void DrawUI() { }

    }
}
