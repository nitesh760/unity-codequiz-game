using UnityEngine;
using UnityEngine.SceneManagement;

public class AccountPageController : MonoBehaviour
{
    public void GoToSignupPage()
    {
        SceneManager.LoadScene("signup");
    }

    public void GoToLoginPage()
    {
        SceneManager.LoadScene("login");
    }
}
