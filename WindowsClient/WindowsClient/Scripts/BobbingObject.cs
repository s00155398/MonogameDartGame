using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Engine.Base;
using Microsoft.Xna.Framework;

namespace WindowsClient.Scripts
{
    public class BobbingObject : ScriptComponent
    {
        public float BobbingAmount {get;set;}
        Vector3 StartLocation;
        float MinX, MaxX;
        bool isMovingUp = true;

        public BobbingObject(float bobbingHeight) 
            : base()
        {
            BobbingAmount = bobbingHeight;
        }

        public override void Initialize()
        {
            StartLocation = Manager.Owner.Location;

            MinX = StartLocation.X - BobbingAmount;
            MaxX = StartLocation.X + BobbingAmount;

            base.Initialize();
        }

        public override void Update()
        {
            if(isMovingUp == true)
            {
                //has reached MaxY
                if(Manager.Owner.Location.X < MaxX)
                {
                    Manager.Owner.World *=
                        Matrix.CreateTranslation(0.1f, 0, 0);
                }
                else
                {
                    isMovingUp = false;
                }
            }
            else
            {
                //has reached MinY
                if(Manager.Owner.Location.X > MinX)
                {
                    Manager.Owner.World *=
                        Matrix.CreateTranslation(-0.1f, 0, 0);
                }
                else
                {
                    isMovingUp = true;
                }
            }

            base.Update();
        }

       
    }
}
