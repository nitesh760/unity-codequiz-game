using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput, passwordInput;
    public TextMeshProUGUI statusText;

    private string filePath;

    void Start()
    {
        filePath = UnityEngine.Application.persistentDataPath + "/users.json";
    }

    public void LoginUser()
    {
        string enteredUsername = usernameInput.text.Trim();
        string enteredPassword = passwordInput.text.Trim();

        if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))
        {
            statusText.text = "Enter username & password!";
            return;
        }

        string json = File.ReadAllText(filePath);
        UserDatabase database = JsonUtility.FromJson<UserDatabase>(json);

        foreach (UserData user in database.users)
        {
            if (user.username == enteredUsername && user.password == enteredPassword)
            {
                // Save the logged-in username
                PlayerPrefs.SetString("LoggedInUser", enteredUsername);
                PlayerPrefs.Save();

                SceneManager.LoadScene("main menu");  // Redirect to Menu Page
                return;
            }
        }

        statusText.text = "Invalid username or password!";
    }
    public void OpenForgetPasswordPage()
    {
        SceneManager.LoadScene("ForgetPasswordPage");  // Ensure the scene name is correct
    }
}
