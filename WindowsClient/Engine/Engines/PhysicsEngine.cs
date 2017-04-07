using BEPUphysics;
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.Entities;

using Engine.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Engines
{
    public class PhysicsEngine : GameComponent
    {
        private static Space SimulationSpace;

        public PhysicsEngine(Game game)
            :base(game)
        {
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            SimulationSpace = new Space();
            SimulationSpace.ForceUpdater.Gravity =
                new BEPUutilities.Vector3(0, -12, 0);

            base.Initialize();
        }

        public static void AddEntity(Entity entity)
        {
            if (entity != null)
                SimulationSpace.Add(entity);
        }

        public static void RemoveEntity(Entity entity)
        {
            if (entity != null)
                SimulationSpace.Remove(entity);
        }

        public override void Update(GameTime gameTime)
        {
            SimulationSpace.Update();

            base.Update(gameTime);
        }

        public static void AddStaticMesh(StaticMesh mesh)
        {
            if (mesh != null)
                SimulationSpace.Add(mesh);
        }

        public static void RemoveStaticMesh(StaticMesh mesh)
        {
            if (mesh != null)
                SimulationSpace.Remove(mesh);
        }

        public static void CastRay(Ray ray, float maxDistance, bool filterStaticMeshes,
            out PhysicsComponent.GameObjectInfo info)
        {
            RayCastResult result = new RayCastResult();
            info = null;

            if(filterStaticMeshes)
            {
                SimulationSpace.RayCast(
                    MathConverter.Convert(ray),
                    maxDistance,
                    FilterStaticMesh,
                    out result);
            }
            else
            {
                SimulationSpace.RayCast(
                    MathConverter.Convert(ray),
                    maxDistance,
                    out result);
            }

            if(result.HitObject != null)
            {
                if(result.HitObject is Collidable)
                {
                    var collidable = result.HitObject as Collidable;

                    if (collidable.Tag != null)
                        if (collidable.Tag is PhysicsComponent.GameObjectInfo)
                        {
                            info = collidable.Tag as PhysicsComponent.GameObjectInfo;
                        }
                }
            }
        }

        public static void CastRay(Ray ray, float maxDistance, bool filterStaticMeshes,
    out RayCastResult result)
        {
            result = new RayCastResult();

            if (filterStaticMeshes)
            {
                SimulationSpace.RayCast(
                    MathConverter.Convert(ray),
                    maxDistance,
                    FilterStaticMesh,
                    out result);
            }
            else
            {
                SimulationSpace.RayCast(
                    MathConverter.Convert(ray),
                    maxDistance,
                    out result);
            }
        }

        private static bool FilterStaticMesh(BroadPhaseEntry entry)
        {
            if(entry is Collidable)
            {
                if ((entry as Collidable) is StaticMesh)
                    return false;
                else return true;
            }

            return false;
        }
    }
}
