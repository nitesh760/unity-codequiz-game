using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidBackButtonHandler : MonoBehaviour
{
    public GameObject quitPopup; // Assign this in the Inspector

    void Update()
    {
        // Check if the back button is pressed
        if (Input.GetButtonDown("Cancel")) // Works for Android back button
        {
            HandleBackButton();
        }
    }

    void HandleBackButton()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "main menu":
                // Show quit confirmation popup instead of quitting immediately
                if (quitPopup != null)
                {
                    quitPopup.SetActive(true);
                }
                else
                {
                    UnityEngine.Debug.LogWarning("Quit popup is not assigned in the Inspector.");
                }
                break;

            case "easy":
            case "medium":
            case "hard":
            case "test":
            case "c":
            case "c++":
            case "java":
            case "python":
            case "html":
            case "css":
            case "sql":
            case "dart":
            case "R":
            case "csharp":
            case "kotlin":
            case "java script":
            case "Rust":
            case "Ruby":
            case "Go":
            case "swift":
            case "php":
            case "Pl sql":
            case "matlab":
            case "bash":
                SceneManager.LoadScene("playpage");
                break;

            case "playpage":
            case "AccountPage":
            case "setting":
                SceneManager.LoadScene("main menu");
                break;

            case "signup":
            case "login":
                SceneManager.LoadScene("AccountPage");
                break;

            case "ForgetPasswordPage":
                SceneManager.LoadScene("login");
                break;

            case "ResetPasswordPage":
                SceneManager.LoadScene("ForgetPasswordPage");
                break;

            case "benifits":
                SceneManager.LoadScene("setting");
                break;

            default:
                UnityEngine.Debug.LogWarning("Unhandled scene: " + currentScene);
                break;
        }
    }
}
