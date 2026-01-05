using System.Diagnostics;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class QuitGame : MonoBehaviour
{
    public GameObject quitPopup; // This should now appear in the Inspector

    public void ShowQuitPopup()
    {
        quitPopup.SetActive(true); // Show the quit confirmation panel
    }

    public void CancelQuit()
    {
        quitPopup.SetActive(false); // Hide the panel
    }

    public void quitgame()
    {
        UnityEngine.Application.Quit(); // Quit the game (Only works in a built version)
        UnityEngine.Debug.Log("Game is quitting..."); // Debug for testing in the editor
    }
}
