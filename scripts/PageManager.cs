using UnityEngine;
using UnityEngine.SceneManagement;

public class PageManager : MonoBehaviour
{
    // Navigate to Sign-Up page
    public void GoToSignupPage()
    {
        SceneManager.LoadScene("account");
    }

    // Navigate to Login page
    public void GoToLoginPage()
    {
        SceneManager.LoadScene("login");
    }

    // Navigate to Forgot Password page
    public void GoToForgotPasswordPage()
    {
        SceneManager.LoadScene("ForgotPassword");
    }

    // Navigate to Reset Password page
    public void GoToResetPasswordPage()
    {
        SceneManager.LoadScene("ResetPassword");
    }
}
