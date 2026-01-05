using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class SignupManager : MonoBehaviour
{
    public TMP_InputField usernameInput, passwordInput, emailInput;
    public TextMeshProUGUI statusText;

    private string filePath;

    void Start()
    {
        filePath = UnityEngine.Application.persistentDataPath + "/users.json";
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(new UserDatabase()));
        }
    }

    public void RegisterUser()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text.Trim();
        string email = emailInput.text.Trim();

        // Validate Email Format (@gmail.com required)
        if (!email.EndsWith("@gmail.com"))
        {
            statusText.text = "Email must end with @gmail.com!";
            return;
        }

        // Validate Password Strength
        if (!IsValidPassword(password))
        {
            statusText.text = "Password must be 8+ characters, include uppercase, lowercase, and a number!";
            return;
        }

        string json = File.ReadAllText(filePath);
        UserDatabase database = JsonUtility.FromJson<UserDatabase>(json);

        foreach (UserData user in database.users)
        {
            if (user.username == username || user.email == email)
            {
                statusText.text = "Username or Email already exists!";
                return;
            }
        }

        database.users.Add(new UserData { username = username, password = password, email = email });
        File.WriteAllText(filePath, JsonUtility.ToJson(database));
        statusText.text = "Signup Successful! Redirecting...";

        // Redirect to Menu Page
        UnityEngine.SceneManagement.SceneManager.LoadScene("main menu");
    }

    private bool IsValidPassword(string password)
    {
        // Must be at least 8 characters long, contain uppercase, lowercase, and a number
        return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");
    }
}
