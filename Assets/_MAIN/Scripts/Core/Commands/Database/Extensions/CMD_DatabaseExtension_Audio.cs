using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace COMMANDS
{

    public class CMD_DatabaseExtension_Audio : CMD_DatabaseExtension
    {
        private static string[] PARAM_SFX = new string[] { "-s", "-sfx" };
        private static string[] PARAM_VOLUME = new string[] { "-v", "-vol", "-volume" };
        private static string[] PARAM_PITH = new string[] { "-p", "-pitch" };
        private static string[] PARAM_LOOP = new string[] { "-l", "-loop" };

        private static string[] PARAM_CHANNEL = new string[] { "-c", "-channel" };
        private static string[] PARAM_IMMEDIATE = new string[] { "-i", "-immediate" };
        private static string[] PARAM_START_VOLUME = new string[] { "-sv", "-startvolume" };
        private static string[] PARAM_SONG = new string[] { "-s", "-song" };
        private static string[] PARAM_AMBIENCE = new string[] { "-a", "-ambience" };

        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("playsfx", new Action<string[]>(PlaySFX));
            database.AddCommand("stopsfx", new Action<string>(StopSFX));

            database.AddCommand("playvoice", new Action<string[]>(PlayVoice));
            database.AddCommand("stopvoice", new Action<string>(StopSFX));

            database.AddCommand("playsong", new Action<string[]>(PlaySong));
            database.AddCommand("playambience", new Action<string[]>(PlayAmbience));

            database.AddCommand("stopsong", new Action<string>(StopSong));
            database.AddCommand("stopambience", new Action<string>(StopAmbince));

        }
        private static void PlaySFX(string[] data)
        {
            string filepath;
            float volume, pitch;
            bool loop;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_SFX, out filepath);
            parameters.TryGetValue(PARAM_VOLUME, out volume, defaultValue: 1f);
            parameters.TryGetValue(PARAM_PITH, out pitch, defaultValue: 1f);
            parameters.TryGetValue(PARAM_LOOP, out loop, defaultValue: false);

            AudioClip sound = Resources.Load<AudioClip>(FilePaths.GetPathToResource(FilePaths.resources_sfx, filepath));

            if (sound == null)
                return;

            AudioManager.instance.PlaySoundEffect(sound, volume: volume, pitch: pitch, loop: loop);

        }
        private static void StopSFX(string data)
        {
            AudioManager.instance.StopSoundEffect(data);
        }

        private static void PlayVoice(string[] data)
        {
            string filepath;
            float volume, pitch;
            bool loop;

            var parameters = ConvertDataToParameters(data);
            parameters.TryGetValue(PARAM_SFX, out filepath);
            parameters.TryGetValue(PARAM_VOLUME, out volume, defaultValue: 1f);
            parameters.TryGetValue(PARAM_PITH, out pitch, defaultValue: 1f);
            parameters.TryGetValue(PARAM_LOOP, out loop, defaultValue: false);

            AudioClip sound = Resources.Load<AudioClip>(FilePaths.GetPathToResource(FilePaths.resources_voices, filepath));

            if (sound == null)
                return;

            AudioManager.instance.PlayVoice(sound, volume: volume, pitch: pitch, loop: loop);
        }
        private static void PlaySong(string[] data)
        {
            string filepath;
            int channel;

            var parameters = ConvertDataToParameters(data);
            parameters.TryGetValue(PARAM_SONG, out filepath);
            filepath = FilePaths.GetPathToResource(FilePaths.resources_music, filepath);

            parameters.TryGetValue(PARAM_CHANNEL, out channel, defaultValue: 1);

            PlayTrack(filepath, channel, parameters);

        }
        private static void PlayAmbience(string[] data)
        {
            string filepath;
            int channel;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_AMBIENCE, out filepath);
            filepath = FilePaths.GetPathToResource(FilePaths.resources_ambience, filepath);

            parameters.TryGetValue(PARAM_CHANNEL, out channel, defaultValue: 0);

            PlayTrack(filepath, channel, parameters);

        }

        private static void PlayTrack(string filepath, int channel, CommandParameters parameters)
        {
            bool loop;
            float volumeCap;
            float startvolume;
            float pitch;

            parameters.TryGetValue(PARAM_VOLUME, out volumeCap, defaultValue: 1f);
            parameters.TryGetValue(PARAM_START_VOLUME, out startvolume, defaultValue: 0f);
            parameters.TryGetValue(PARAM_PITH, out pitch, defaultValue: 1f);
            parameters.TryGetValue(PARAM_LOOP, out loop, defaultValue: true);
            AudioClip sound = Resources.Load<AudioClip>(filepath);

            if (sound == null)
            {
                Debug.Log($"Was not able to load voice '{filepath}'");
                return;
            }
            AudioManager.instance.PlayTrack(sound, channel, loop, startvolume, volumeCap, pitch, filepath);
        }

        private static void StopSong(string data)
        {
            if (data == string.Empty)
                StopTrack("1");
            else
                StopTrack(data);

        }
        private static void StopAmbince(string data)
        {
            if (data == string.Empty)
                StopTrack("0");
            else
                StopTrack(data);

        }
        private static void StopTrack(string data)
        {
            if (int.TryParse(data, out int channel))
                AudioManager.instance.StopTrack(channel);
            else
                AudioManager.instance.StopTrack(data);
        }

    }
}

