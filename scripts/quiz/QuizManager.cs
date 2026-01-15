using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public void LoadQuiz(string quizName)
    {
        // Load the quiz scene based on the selected language
        SceneManager.LoadScene(quizName);
    }
}
