using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Engine.Base;
using Engine.Engines;

using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

//NuGet Newtonsoft.Json

namespace Engine
{
    public static class GameUtilities
    {
        public static GraphicsDevice GraphicsDevice { get; set; }
        public static GameTime Time { get; set; }
        public static ContentManager Content { get; set; }
        public static Random Random { get; set; }
        public static SpriteFont DebugFont { get; set; }
        public static SpriteBatch DebugSpriteBatch { get; set; }

        public static Color DebugTextColor { get; set; }
        public static bool GameHasFocus { get; set; }

        public static void SaveToXML(object objectToSave, Type objectType,
            string path)
        {
            using (var stream = File.Create(path))
            {
                XmlSerializer xs = new XmlSerializer(objectType);
                xs.Serialize(stream, objectToSave);
            }
        }

        public static object LoadFromXML(string path, Type objectType)
        {
            if (File.Exists(path))
                using (var stream = File.Open(path, FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(objectType);
                    return xs.Deserialize(stream);
                }
            else return null;
        }

        //public static string SerializeToJSON(object objectToSave)
        //{
        //    try
        //    {
        //        return JsonConvert.SerializeObject(objectToSave);
        //    }
        //    catch (Exception ex)
        //    {
        //        return string.Empty;
        //    }
        //}

        //public static T DeserializeFromJSON<T>(string jsonData)
        //{
        //    try
        //    {
        //        return JsonConvert.DeserializeObject<T>(jsonData);
        //    }
        //    catch
        //    {
        //        return default(T);
        //    }
        //}

        public static Vector3 PickRandomPosition(int min, int max)
        {
            return new Vector3(
                Random.Next(min, max),
                Random.Next(min, max),
                Random.Next(min, max));
        }

        public static Color PickRandomColor()
        {
            return new Color(
                Random.Next(1, 255),
                Random.Next(1, 255),
                Random.Next(1, 255));
        }

        public static void SetGraphicsDeviceFor3D()
        {
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
        }

        public static Ray CreateRayFromVector2(Vector2 screenPosition)
        {
            #region 2D Point to 3D Point
            Vector3 nearScreenPoint = new Vector3(
                screenPosition.X,
                screenPosition.Y, 
                0);

            Vector3 farScreenPoint = new Vector3(
                screenPosition.X,
                screenPosition.Y,
                1);

            Vector3 near3DPoint = GraphicsDevice.Viewport.Unproject(
                nearScreenPoint,
                CameraEngine.ActiveCamera.Projection,
                CameraEngine.ActiveCamera.View,
                Matrix.Identity);

            Vector3 far3DPoint = GraphicsDevice.Viewport.Unproject(
                farScreenPoint,
                CameraEngine.ActiveCamera.Projection,
                CameraEngine.ActiveCamera.View,
                Matrix.Identity);
            #endregion

            Vector3 rayDirection = far3DPoint - near3DPoint;
            rayDirection.Normalize();

            return new Ray(near3DPoint, rayDirection);
        }

        public static float? DoesIntersectWith(Ray ray, BoundingBox box)
        {
            return ray.Intersects(box);
        }

        public static bool DoesIntersectWith(Ray ray, float maxRayDistance, BoundingBox box)
        {
            float? result = DoesIntersectWith(ray, box);

            if (result == null)
                return false;
            else if (result < maxRayDistance)
                return true;
            else
                return false;
        }
    }

}
