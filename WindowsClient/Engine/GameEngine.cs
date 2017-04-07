using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Engine.Engines;
using Engine.Base;

namespace Engine
{
    public class GameEngine : DrawableGameComponent
    {
        InputEngine input;
        CameraEngine camera;
        FrameRateCounter fpsCounter;
        DebugEngine debug;
        PhysicsEngine physics;
        AudioEngine audio;

        private Scene activeScene;
        public Scene ActiveScene { get { return activeScene; } }

        public GameEngine(Game game) :
            base(game)
        {
            game.Components.Add(this);

            input = new InputEngine(game);
            camera = new CameraEngine(game);
            physics = new PhysicsEngine(game);

            fpsCounter = new FrameRateCounter(game);
            audio = new AudioEngine(game);

            debug = new DebugEngine();
        }

        public override void Initialize()
        {
            debug.Initialize();
           
            base.Initialize();
        }

        public void LoadScene(Scene newScene)
        {
            if(newScene != null)
            {
                if (activeScene != null)
                    UnloadScene();

                activeScene = newScene;
                activeScene.Initialize();
            }
        }

        public void UnloadScene()
        {
            //destroy all objects -> get rid of entities in simulation
            activeScene.Unload();
            activeScene = null;
            CameraEngine.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            if (activeScene != null)
                activeScene.Update();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (activeScene != null)
            {
                if (CameraEngine.ActiveCamera != null)
                    activeScene.Draw3D(CameraEngine.ActiveCamera);

                activeScene.DrawUI();

                if (System.Diagnostics.Debugger.IsAttached)
                    debug.Draw(CameraEngine.ActiveCamera);

                GameUtilities.SetGraphicsDeviceFor3D();
            }

            base.Draw(gameTime);
        }
    }
}
