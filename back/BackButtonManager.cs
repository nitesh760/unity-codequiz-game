using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonManager : MonoBehaviour
{
    public string previousSceneName; // Set this in the Inspector

    public void GoBack()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            UnityEngine.Debug.LogError("Previous scene name not set in Inspector!");
        }
    }
}
