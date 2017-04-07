using Engine.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Engines
{
    public class CameraEngine : GameComponent
    {
        private static Dictionary<string, CameraComponent> cameras;
        private static CameraComponent activeCamera;
        private static string activeCameraID;

        public static CameraComponent ActiveCamera
        {
            get { return activeCamera; }
        }

        public CameraEngine(Game game) 
            : base(game)
        {
            cameras = new Dictionary<string, CameraComponent>();
            game.Components.Add(this);
        }

        //all are based on Dictionary<string, CameraComponent>
        //use the Dictionary Sample on Moodle for dictionary usage
        public static void SetActiveCamera(string id)
        {
            if(activeCameraID != id)
            {
                if(cameras.ContainsKey(id))
                {
                    activeCamera = cameras[id];
                }
            }
        }

        public static void AddCamera(CameraComponent camera)
        {
            if(!cameras.ContainsKey(camera.ID))
            {
                cameras.Add(camera.ID, camera);

                if (cameras.Count == 1)
                    SetActiveCamera(camera.ID);
            }
        }

        public static void Clear()
        {
            cameras.Clear();
            activeCamera = null;
            activeCameraID = string.Empty;
        }

        public static void RemoveCamera(string id)
        {
            if(activeCameraID == id)
            {
                activeCameraID = string.Empty;
                activeCamera = null;
            }

            if(cameras.ContainsKey(id))
            {
                cameras.Remove(id);
            }
        }

        public static List<string> GetCurrentCameras()
        {
            return cameras.Keys.ToList();
        }
    }
}
