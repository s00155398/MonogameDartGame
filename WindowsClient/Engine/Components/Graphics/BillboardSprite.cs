using Engine.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Components.Graphics
{
    public class BillboardSprite : RenderComponent
    {
        string _asset;
        Texture2D texture;
        BasicEffect effect;
        Vector2 origin;
        SpriteBatch sbatch;

        public BillboardSprite(string asset) 
            : base()
        {
            _asset = asset;
        }

        public override void Initialize()
        {
            texture = GameUtilities.Content.Load<Texture2D>
                (@"Textures\" + _asset);

            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            sbatch = new SpriteBatch(GameUtilities.GraphicsDevice);
            effect = new BasicEffect(GameUtilities.GraphicsDevice);
            effect.TextureEnabled = true;

            base.Initialize();
        }

        public override void Draw(CameraComponent camera)
        {
            effect.World = Matrix.CreateConstrainedBillboard(
                Manager.Owner.Location,
                camera.Manager.Owner.Location,
                Vector3.Down,
                null, null);

            effect.View = camera.View;
            effect.Projection = camera.Projection;

            sbatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend, null, null, null, effect);

            sbatch.Draw(
                texture, 
                Vector2.Zero,
                null,
                Color.White,
                0,
                origin, 
                0.01f,
                SpriteEffects.None,
                0);

            sbatch.End();
            GameUtilities.SetGraphicsDeviceFor3D();
            
                base.Draw(camera);
        }
    }
}
