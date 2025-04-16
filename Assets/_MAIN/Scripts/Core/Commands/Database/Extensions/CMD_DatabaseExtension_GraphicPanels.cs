using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Video;
using Unity.VisualScripting;

namespace COMMANDS
{
    public class CMD_DatabaseExtension_GraphicPanels : CMD_DatabaseExtension
    {
        private static string[] PARAM_PANEL = new string[] { "-p", "-panel" };
        private static string[] PARAM_LAYER = new string[] { "-l", "-layer" };
        private static string[] PARAM_MEDIA = new string[] { "-m", "-media" };
        private static string[] PARAM_GRAPHIC = new string[] { "-g", "-graphic" };
        private static string[] PARAM_SPEED = new string[] { "-spd", "-speed" };
        private static string[] PARAM_IMMEDIATE = new string[] { "-i", "-immediate" };
        private static string[] PARAM_BLENDTEX = new string[] { "-b", "-blendtex" };
        private static string[] PARAM_USEVIDEOAUDIO = new string[] { "-aud", "-audio" };

        private const string HOME_DIRECTORY_SYMBOL = "~/";
        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("setlayermedia", new Func<string[], IEnumerator>(SetLayerMedia));
            database.AddCommand("Clearlayermedia", new Func<string[], IEnumerator>(ClearLayerMedia));
        }

        private static IEnumerator SetLayerMedia(string[] data)
        {
            string panelName = "";
            int layer = 0;
            string mediaName = "";
            float transitionSpeed = 0;
            bool immediate = false;
            string blendTextName = "";
            bool useAudio = false;

            string pathToGraphic = "";
            UnityEngine.Object graphic = null;
            Texture blendTex = null;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_PANEL, out panelName);
            GraphicPanel panel = GraphicPanelManager.instance.GetPanel(panelName);
            if (panel == null)
            {
                Debug.LogError($"Unable to grab panel '{panelName}' because it is not valid panel. Please check the panel name ");
                yield break;
            }

            parameters.TryGetValue(PARAM_LAYER, out layer, defaultValue: 0);
            parameters.TryGetValue(PARAM_MEDIA, out mediaName);
            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);

            if (!immediate)
                parameters.TryGetValue(PARAM_SPEED, out transitionSpeed, defaultValue: 1);

            parameters.TryGetValue(PARAM_BLENDTEX, out blendTextName);
            parameters.TryGetValue(PARAM_USEVIDEOAUDIO, out useAudio, defaultValue: false);

            //logic
            pathToGraphic = GetPathToGraphic(FilePaths.resources_backgroundImages, mediaName);
            graphic = Resources.Load<Texture>(pathToGraphic);

            if (graphic == null)
            {
                pathToGraphic = GetPathToGraphic(FilePaths.resources_backgroundVideo, mediaName);
                graphic = Resources.Load<VideoClip>(pathToGraphic);
            }

            if (graphic == null)
            {
                Debug.LogError($"Could not find media file callde'{mediaName}' in the resources directories");
                yield break;
            }
            if (!immediate && blendTextName != string.Empty)
                blendTex = Resources.Load<Texture>(FilePaths.resources_blendTextures + blendTextName);

            GraphicLayer graphicLayer = panel.GetLayer(layer, createIfDoesNotExist: true);

            if (graphic is Texture)
            {
                yield return graphicLayer.SetTexture(graphic as Texture, transitionSpeed, blendTex, pathToGraphic, immediate);
            }


        }

        private static IEnumerator ClearLayerMedia(string[] data)
        {
            string panelName = "";
            int layer = 0;
            float transitionSpeed = 0;
            bool immediate = false;
            string blendTextName = "";
            Texture blendTex = null;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_PANEL, out panelName);
            GraphicPanel panel = GraphicPanelManager.instance.GetPanel(panelName);
            if (panel == null)
            {
                Debug.LogError($"Unable to grab panel '{panelName}' because it is not a valid panel.");
                yield break;
            }

            parameters.TryGetValue(PARAM_LAYER, out layer, defaultValue: -1);
            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);

            if (!immediate)
                parameters.TryGetValue(PARAM_SPEED, out transitionSpeed, defaultValue: 1);

            parameters.TryGetValue(PARAM_BLENDTEX, out blendTextName);

            if (!immediate && blendTextName != string.Empty)
                blendTex = Resources.Load<Texture>(FilePaths.resources_blendTextures + blendTextName);

            if (layer == -1)
                panel.Clear(transitionSpeed, blendTex, immediate);
            else
            {
                GraphicLayer graphicLayer = panel.GetLayer(layer);
                if (graphicLayer == null)
                {
                    Debug.LogError($"Could not clear layer [{layer}] on panel '{panel.panelName}'");
                    yield break;
                }

                graphicLayer.Clear(transitionSpeed, blendTex, immediate);
            }
        }
        private static string GetPathToGraphic(string defaultPath, string graphicName)
        {
            if (graphicName.StartsWith(HOME_DIRECTORY_SYMBOL))
                return graphicName.Substring(HOME_DIRECTORY_SYMBOL.Length);
            return defaultPath + graphicName;
        }
    }
}

