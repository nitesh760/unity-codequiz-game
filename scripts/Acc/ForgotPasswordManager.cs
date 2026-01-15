using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;

public class ForgotPasswordManager : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TextMeshProUGUI otpMessageText;
    public Button resetPasswordButton;

    private string filePath;
    private string generatedOTP;
    private string userEmail;

    void Start()
    {
        filePath = UnityEngine.Application.persistentDataPath + "/users.json";
        resetPasswordButton.interactable = false;
    }

    public void GenerateOTP()
    {
        userEmail = emailInput.text.Trim();

        string json = File.ReadAllText(filePath);
        UserDatabase database = JsonUtility.FromJson<UserDatabase>(json);

        bool emailExists = false;

        foreach (UserData user in database.users)
        {
            if (user.email == userEmail)
            {
                emailExists = true;
                break;
            }
        }

        if (!emailExists)
        {
            otpMessageText.text = "Email not found!";
            return;
        }

        generatedOTP = UnityEngine.Random.Range(100000, 999999).ToString();
        UnityEngine.Debug.Log("Generated OTP: " + generatedOTP);  // Show in Unity Console
        otpMessageText.text = "Your OTP: " + generatedOTP;

        //  Save OTP & Email in PlayerPrefs
        PlayerPrefs.SetString("SavedOTP", generatedOTP);
        PlayerPrefs.SetString("UserEmail", userEmail);
        PlayerPrefs.Save();

        resetPasswordButton.interactable = true;
    }

    public void GoToResetPasswordPage()
    {
        SceneManager.LoadScene("ResetPasswordPage");
    }
}
