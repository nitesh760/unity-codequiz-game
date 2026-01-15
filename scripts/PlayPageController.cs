using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayPageController : MonoBehaviour
{
    public void StartEasyLevel()
    {
        UnityEngine.Debug.Log("Easy Level Selected");
        // Add code to start the Easy level
        SceneManager.LoadScene("easy");
    }

    public void StartMediumLevel()
    {
        UnityEngine.Debug.Log("Medium Level Selected");
        // Add code to start the Medium level
        SceneManager.LoadScene("medium");
    }

    public void StartHardLevel()
    {
        UnityEngine.Debug.Log("Hard Level Selected");
        // Add code to start the Hard level
        SceneManager.LoadScene("hard");
    }

    public void Selectyourchoice()
    {
        UnityEngine.Debug.Log("your choice selected");
        // Add code to start the Hard level
        SceneManager.LoadScene("selectquiz");
    }

}
