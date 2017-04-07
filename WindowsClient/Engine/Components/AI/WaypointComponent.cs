using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Engine.Engines;
using Engine.Base;

namespace Engine.Components.AI
{
    public class WaypointComponent : Component
    {
        public event ObjectStringIDHandler Complete;

        public Queue<Vector3> Waypoints;
        public Vector3 Destination;
        public Vector3 Speed = new Vector3(10f, 10f, 10f);
        public float AcceptableDistance = 5f;

        private string waypointSource;
        private bool isPhysicsEnabled = false;
        private bool loadFromModel = false;
        public bool DoMove = false;
        public bool Looping = true;

        public WaypointComponent(string asset)
            : base()
        {
            waypointSource = asset;
            Waypoints = new Queue<Vector3>();
            loadFromModel = true;
        }

        public WaypointComponent(Queue<Vector3> points)
            : base()
        {
            Waypoints = points;
            waypointSource = null;
            loadFromModel = false;
        }

        public WaypointComponent()
    : base()
        {
            Waypoints = new Queue<Vector3>();
            waypointSource = null;
            loadFromModel = false;
        }

        public override void Initialize()
        {
            if(loadFromModel)
            {
                if (!string.IsNullOrEmpty(waypointSource))
                {
                    Model waypointModel = GameUtilities.Content.Load<Model>("Waypoints\\" + waypointSource);

                    if (waypointModel.Tag != null)
                    {
                        if (waypointModel.Tag is List<Vector3>)
                        {
                            var points = waypointModel.Tag as List<Vector3>;

                            Waypoints = new Queue<Vector3>(points);

                            Manager.Owner.World *= Matrix.CreateTranslation(-Manager.Owner.Location);
                            Manager.Owner.World *= Matrix.CreateTranslation(Waypoints.Dequeue());

                            SetDestination(Waypoints.Dequeue());

                        }
                    }
                }
            }

            if (Manager.HasComponent<PhysicsComponent>())
            {
                (Manager.GetComponent(typeof(PhysicsComponent)) as PhysicsComponent).Entity.AngularDamping = 0.2f;
                (Manager.GetComponent(typeof(PhysicsComponent)) as PhysicsComponent).Entity.LinearDamping = 0.2f;
                (Manager.GetComponent(typeof(PhysicsComponent)) as PhysicsComponent).Entity.Material.KineticFriction = 0.5f;
                isPhysicsEnabled = true;
            }

            base.Initialize();
        }

        public void AddWaypoint(Vector3 point)
        {
            Waypoints.Enqueue(point);

            if (Waypoints.Count == 1 && Destination == Vector3.Zero)
                NextWaypoint();
        }

        public void ClearWaypoints()
        {
            Waypoints.Clear();
        }

        public override void Update()
        {
            if (DoMove)
            {
                if (!HasReachedDestination())
                {
                    MoveTowardsDestination((float)GameUtilities.Time.ElapsedGameTime.TotalSeconds);
                }
                else
                {
                    if (Looping)
                        AddWaypoint(Destination);

                    if (Waypoints.Count > 0)
                        NextWaypoint();
                    else
                        if (Complete != null)
                        Complete(ID);
                }
            }

            if (System.Diagnostics.Debugger.IsAttached)
            {
                DebugEngine.AddBoundingSphere(new BoundingSphere(Destination, 1.0f), Color.Black);

                foreach (var p in Waypoints)
                {
                    DebugEngine.AddBoundingSphere(new BoundingSphere(p, 1.0f), Color.LawnGreen);
                }
            }

            base.Update();
        }

        private void NextWaypoint()
        {
            SetDestination(Waypoints.Dequeue());
        }

        public void SetDestination(Vector3 dest)
        {
            Destination = dest;
        }

        private bool HasReachedDestination()
        {
            if (Vector3.Distance(Manager.Owner.World.Translation, Destination) < AcceptableDistance)
                return true;
            else
                return false;
        }

        private void MoveTowardsDestination(float deltaTime)
        {
            //Get the Vector3 direction between the current position and the target destination
            var direction = Destination - Manager.Owner.World.Translation;
            direction.Normalize();

            if (isPhysicsEnabled)
            {
                (Manager.GetComponent(typeof(PhysicsComponent)) as PhysicsComponent).Entity.ApplyImpulse(
                    MathConverter.Convert(Manager.Owner.Location), MathConverter.Convert(direction * ((Speed * 10) * deltaTime)));
            }
            else
            {
                //translate our position in the direction of the next target at a fixed speed
                //Why do we divide the speed by the previous frame time?
                Manager.Owner.World *= Matrix.CreateTranslation((direction * (Speed * deltaTime)));
            }
        }

    }
}
