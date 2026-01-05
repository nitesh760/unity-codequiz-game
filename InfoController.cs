using UnityEngine;

using UnityEngine.SceneManagement; // This is required for SceneManager

public class InfoController : MonoBehaviour
{
    
    public void OpenBenefits()
    {
        UnityEngine.Debug.Log("Opening benefits...");
        SceneManager.LoadScene("Benifits");
    }
    public void LoadHowToPlayScene()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}
