using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using System.Threading;

namespace Engine.Engines
{
    public sealed class AudioEngine : GameComponent
    {
        private static Dictionary<string, Song> _songs = new Dictionary<string, Song>();
        private static Dictionary<string, SoundEffect> _effects = new Dictionary<string, SoundEffect>();

        public static Dictionary<string, Song> LoadedSongs { get { return _songs; } }
        public static Dictionary<string, SoundEffect> LoadedEffects { get { return _effects; } }

        public AudioEngine(Game _game)
            : base(_game)
        {
            _game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public static void LoadSong(string name)
        {
            if (GameUtilities.Content != null && !_songs.ContainsKey(name))
            {
                var song = GameUtilities.Content.Load<Song>("Audio\\Songs\\" + name);
                _songs.Add(name, song);
            }
        }

        public static void RemoveSong(string name)
        {
            if (_songs.ContainsKey(name))
                _songs.Remove(name);
        }

        public static void LoadEffect(string name)
        {
            if (GameUtilities.Content != null && !_effects.ContainsKey(name))
            {
                var effect = GameUtilities.Content.Load<SoundEffect>("Audio\\Effects\\" + name);
                _effects.Add(name, effect);
            }
        }

        public static void RemoveEffect(string name)
        {
            if (_effects.ContainsKey(name))
                _effects.Remove(name);
        }

        public static void PlaySong(string name)
        {
            if (_songs.ContainsKey(name))
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(_songs[name]);
            }
        }

        public static void PlayEffect(string name)
        {
            if (_effects.ContainsKey(name))
            {
                _effects[name].Play();
            }
        }
    }
}
