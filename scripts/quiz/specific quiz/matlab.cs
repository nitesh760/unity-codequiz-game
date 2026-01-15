using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class matlab : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] options;
        public int correctAnswerIndex;
    }

    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI feedbackText;
    public Button skipButton;
    public List<Question> questions;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private bool isFeedbackActive = false;
    private List<Question> shuffledQuestions;

    void Start()  // Fixed method name
    {
        DebugReference();

        if (questions == null || questions.Count == 0)
        {
            UnityEngine.Debug.LogError("No questions available!");
            return;
        }

        shuffledQuestions = new List<Question>(questions);
        Shuffle(shuffledQuestions);

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int buttonIndex = i;
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(buttonIndex));
        }

        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(SkipQuestion);

        DisplayQuestion();
        UpdateScore();
        feedbackText.text = "";
    }

    void DebugReference()
    {
        if (questionText == null) UnityEngine.Debug.LogError("Question Text is not assigned!");
        if (optionButtons == null || optionButtons.Length == 0) UnityEngine.Debug.LogError("Option Buttons are not assigned!");
        if (scoreText == null) UnityEngine.Debug.LogError("Score Text is not assigned!");
        if (feedbackText == null) UnityEngine.Debug.LogError("Feedback Text is not assigned!");
        if (skipButton == null) UnityEngine.Debug.LogError("Skip Button is not assigned!");
    }

    void DisplayQuestion()
    {
        if (shuffledQuestions == null || shuffledQuestions.Count == 0)
        {
            UnityEngine.Debug.LogError("No questions available!");
            return;
        }

        if (currentQuestionIndex >= shuffledQuestions.Count)
        {
            questionText.text = "Quiz Finished! Final Score: " + score;

            SaveScore(score); //  Save the final score when the quiz ends!

            foreach (Button button in optionButtons)
            {
                button.gameObject.SetActive(false);
            }
            skipButton.gameObject.SetActive(false);
            feedbackText.text = "";
            return;
        }

        Question currentQuestion = shuffledQuestions[currentQuestionIndex];
        questionText.text = currentQuestion.questionText;

        List<int> optionIndices = new List<int>();
        for (int i = 0; i < currentQuestion.options.Length; i++)
        {
            optionIndices.Add(i);
        }
        Shuffle(optionIndices);

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < currentQuestion.options.Length)
            {
                int shuffledIndex = optionIndices[i];
                TextMeshProUGUI buttonText = optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();

                if (buttonText != null)
                {
                    buttonText.text = currentQuestion.options[shuffledIndex];
                }
                else
                {
                    UnityEngine.Debug.LogError("Button " + i + " is missing a TextMeshProUGUI component!");
                }

                optionButtons[i].name = (shuffledIndex == currentQuestion.correctAnswerIndex) ? "Correct" : "Wrong";
                optionButtons[i].gameObject.SetActive(true);
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }

        feedbackText.text = "";
        isFeedbackActive = false;
    }


    void OnOptionSelected(int selectedIndex)
    {
        if (isFeedbackActive) return;

        Button selectedButton = optionButtons[selectedIndex];
        if (selectedButton.name == "Correct")
        {
            feedbackText.text = "Correct! Moving to the next question...";
            feedbackText.color = Color.green;
            score++;
        }
        else
        {
            Question currentQuestion = shuffledQuestions[currentQuestionIndex];
            feedbackText.text = $"Wrong! The correct answer is: {currentQuestion.options[currentQuestion.correctAnswerIndex]}";
            feedbackText.color = Color.red;
        }

        UpdateScore();
        isFeedbackActive = true;
        Invoke(nameof(NextQuestion), 3f);
    }

    void NextQuestion()
    {

        currentQuestionIndex++;
        DisplayQuestion();


    }
    void SaveScore(int newScore)
    {
        string newScoreEntry = " Matlab Score: " + newScore + " | Date: " + System.DateTime.Now;

        // Retrieve existing scores
        string savedScores = PlayerPrefs.GetString("SavedScores", "");
        List<string> scoreList = new List<string>(savedScores.Split('|'));

        // Add new score
        scoreList.Add(newScoreEntry);

        // Keep only the latest 50 scores
        if (scoreList.Count > 50)
        {
            scoreList.RemoveAt(0); // Remove oldest score
        }

        // Save back to PlayerPrefs
        PlayerPrefs.SetString("SavedScores", string.Join("|", scoreList));
        PlayerPrefs.Save();

        UnityEngine.Debug.Log("Saved Score: " + newScoreEntry);
    }

    void SkipQuestion()
    {
        if (isFeedbackActive) return;
        currentQuestionIndex++;
        DisplayQuestion();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;

    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count);  // Fixed ambiguity
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
