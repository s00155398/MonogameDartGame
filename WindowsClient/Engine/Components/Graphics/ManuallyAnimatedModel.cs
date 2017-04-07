using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Components.Graphics
{
    public class ManuallyAnimatedModel : BasicEffectModel
    {
        private ModelBoneCollection Bones { get { return Model.Bones; } }

        public ManuallyAnimatedModel(string asset)
            : base(asset) { }

        public void UpdateBone(int index, Matrix transform)
        {
            if (index <= boneTransforms.Length)
            {
                Vector3 p = boneTransforms[index].Translation;

                for (int i = index; i < boneTransforms.Length; i++)
                {
                    boneTransforms[i] *= Matrix.CreateTranslation(-p);
                    boneTransforms[i] *= transform;
                    boneTransforms[i] *= Matrix.CreateTranslation(p);
                }
            }
        }

        public void UpdateBone(string boneName, Matrix transform)
        {
            int index = -1;

            var bone = Bones.Where(b => b.Name == boneName).FirstOrDefault();

            if (bone != null)
                index = Bones.IndexOf(bone);

            if (index > -1)
                UpdateBone(index, transform);
        }
    }
}
