using Engine.Base;
using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Components.Input
{
    public class PlayerMovementController : Component
    {
        public float MovementSpeed { get; set; }

        public PlayerMovementController(float speed)
            :base()
        {
            MovementSpeed = speed;
        }

        public override void Update()
        {
            if(InputEngine.IsKeyHeld(Keys.A))
            {
                Manager.Owner.World *=
                    Matrix.CreateTranslation(-MovementSpeed, 0, 0);
            }
            else if(InputEngine.IsKeyHeld(Keys.D))
            {
                Manager.Owner.World *=
                    Matrix.CreateTranslation(MovementSpeed, 0, 0);
            }

            if (InputEngine.IsKeyHeld(Keys.W))
            {
                Manager.Owner.World *=
                    Matrix.CreateTranslation(0, 0, -MovementSpeed);
            }
            else if (InputEngine.IsKeyHeld(Keys.S))
            {
                Manager.Owner.World *=
                    Matrix.CreateTranslation(0, 0, MovementSpeed);
            }

            if (InputEngine.IsKeyHeld(Keys.Up))
            {
                Manager.Owner.World *=
                    Matrix.CreateTranslation(0, MovementSpeed, 0);
            }
            else if (InputEngine.IsKeyHeld(Keys.Down))
            {
                Manager.Owner.World *=
                    Matrix.CreateTranslation(0, -MovementSpeed, 0);
            }
            base.Update();
        }
    }
}
