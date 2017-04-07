using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Engine.Components.Cameras;
using Engine.Engines;
using Engine.Base;
using Engine.Animation;

namespace Engine.Components.Graphics
{
    public class SkinnedEffectModel : RenderComponent
    {
        private string asset;
        private string startingAnimation;

        public Model Model { get; set; }
        private SkinningData Data { get; set; }
        public AnimationPlayer Player { get; set; }

        private Matrix[] currentBoneTransforms;

        public SkinnedEffectModel(string asset, string startingAnimation)
            : base()
        {
            this.asset = asset;
            this.startingAnimation = startingAnimation;
        }

        public override void Initialize()
        {
            if (!string.IsNullOrEmpty(asset))
            {
                Model = GameUtilities.Content.Load<Model>("Animated Models/" + asset);

                //Required for monogame 3.5 and below
                //comment this out if using your own machine
               // ApplySkinnedEffect();

                if(Model.Tag != null)
                {
                    if (Model.Tag is SkinningData)
                    {
                        Data = Model.Tag as SkinningData;
                        Player = new AnimationPlayer(Data);
                        PlayAnimation(startingAnimation);
                    }
                }
            }

            base.Initialize();
        }

        private void ApplySkinnedEffect()
        {
            foreach(var mesh in Model.Meshes)
            {
                foreach(var part in mesh.MeshParts)
                {
                    SkinnedEffect seffect = new SkinnedEffect(GameUtilities.GraphicsDevice);
                    BasicEffect originalEffect = part.Effect as BasicEffect;

                    seffect.Texture = originalEffect.Texture;
                    seffect.SpecularColor = originalEffect.SpecularColor;
                    seffect.DiffuseColor = originalEffect.DiffuseColor;

                    part.Effect = seffect;
                }
            }
        }

        public void PlayAnimation(string name)
        {
            if (Player != null)
                if (Data.AnimationClips.ContainsKey(name))
                    Player.StartClip(Data.AnimationClips[name]);
        }

        public override void Update()
        {
            Player.Update(GameUtilities.Time.ElapsedGameTime, true, Manager.Owner.World);

            base.Update();
        }

        public override void Draw(CameraComponent camera)
        {
            if (Player != null)
            {
                currentBoneTransforms = Player.GetSkinTransforms();

                foreach (ModelMesh mesh in Model.Meshes)
                {
                    foreach (SkinnedEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();

                        effect.View = camera.View;
                        effect.Projection = camera.Projection;
                        effect.SetBoneTransforms(currentBoneTransforms);
                    }
                    mesh.Draw();
                }
            }
            base.Draw(camera);
        }
    }
}
