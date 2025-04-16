using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    // Function to quit the application
    public void QuitApplication()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
    public void Quit()
    {
        Application.Quit();
    }

    // Function to change to a specific scene
    public void ChangeScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("Scene name is null or empty. Cannot change scene.");
            return;
        }

        Debug.Log($"Changing to scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
