using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class ResetPasswordManager : MonoBehaviour
{
    public TMP_InputField otpInput, newPasswordInput;
    public TextMeshProUGUI statusText;

    private string filePath;
    private string savedOTP;
    private string userEmail;

    void Start()
    {
        filePath = UnityEngine.Application.persistentDataPath + "/users.json";

        //  Retrieve OTP & Email from PlayerPrefs
        savedOTP = PlayerPrefs.GetString("SavedOTP", "");
        userEmail = PlayerPrefs.GetString("UserEmail", "");
    }

    public void ResetPassword()
    {
        string enteredOTP = otpInput.text.Trim();
        string newPassword = newPasswordInput.text.Trim();

        if (string.IsNullOrEmpty(enteredOTP) || string.IsNullOrEmpty(newPassword))
        {
            statusText.text = "Fill all fields!";
            return;
        }

        // Check if entered OTP matches saved OTP
        if (enteredOTP != savedOTP)
        {
            statusText.text = "Invalid OTP!";
            return;
        }

        // Validate password strength
        if (!IsValidPassword(newPassword))
        {
            statusText.text = "Password must be 8+ chars, include uppercase, lowercase, and a number!";
            return;
        }

        string json = File.ReadAllText(filePath);
        UserDatabase database = JsonUtility.FromJson<UserDatabase>(json);

        foreach (UserData user in database.users)
        {
            if (user.email == userEmail)
            {  // Match email from PlayerPrefs
                user.password = newPassword;
                File.WriteAllText(filePath, JsonUtility.ToJson(database));
                statusText.text = "Password Reset Successfully! Redirecting...";

                // Clear OTP after reset
                PlayerPrefs.DeleteKey("SavedOTP");
                PlayerPrefs.DeleteKey("UserEmail");
                PlayerPrefs.Save();

                SceneManager.LoadScene("login");
                return;
            }
        }

        statusText.text = "Error Resetting Password!";
    }

    private bool IsValidPassword(string password)
    {
        return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");
    }
}
