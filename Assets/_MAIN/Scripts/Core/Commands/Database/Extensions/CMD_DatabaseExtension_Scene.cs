using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


namespace COMMANDS
{
    public class CMD_DatabaseExtension_Scene : CMD_DatabaseExtension
    {
        private static string[] PARAM_IMMEDIATE => new string[] { "-i", "-immediate" };

        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("loadscene", new Func<string[], IEnumerator>(LoadScene));
        }

        private static IEnumerator LoadScene(string[] data)
        {
            string sceneName = data.Length > 0 ? data[0] : string.Empty;
            bool immediate = false;

            var parameters = ConvertDataToParameters(data);
            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);

            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning("No scene name provided for 'loadscene' command.");
                yield break;
            }

            if (immediate)
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
                while (!asyncLoad.isDone)
                {
                    yield return null;
                }
            }
        }
    }
}
