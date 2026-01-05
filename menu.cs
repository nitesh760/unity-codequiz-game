using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

public class menu : MonoBehaviour
{
    public TextMeshProUGUI accountButtonText;  // Fix: Correct type for button text

    void Start()
    {
        string loggedInUser = PlayerPrefs.GetString("LoggedInUser", "Account");
        accountButtonText.text = loggedInUser;  // Fix: Assign text properly
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("playpage");
    }

    public void OpenAccount()
    {
        UnityEngine.Debug.Log("OpenAccount() triggered successfully!");
        SceneManager.LoadScene("AccountPage");
    }

    

    public void Openinfo()
    {
        SceneManager.LoadScene("info");
    }
}
