using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HardQuiz : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public Sprite questionImage;       // Image for the question
        public string questionText;        // Text for the question
        public string[] options;           // Answer options
        public int correctAnswerIndex;     // Index of the correct answer
    }

    public UnityEngine.UI.Image questionImage; // For displaying the question image
    public TextMeshProUGUI questionText;      // For displaying the question text
    public Button[] optionButtons;           // Buttons for the answer options
    public TextMeshProUGUI scoreText;        // For displaying the score
    public TextMeshProUGUI feedbackText;     // For displaying feedback
    public Button skipButton;                // Skip button
   
    public List<Question> questions;         // List of questions

    private int currentQuestionIndex = 0;    // Tracks the current question
    private int score = 0;                   // Tracks the user's score
    private bool isFeedbackActive = false;   // Prevents interaction during feedback

    private List<Question> shuffledQuestions; // Stores shuffled questions

    void Start()
    {
        // Debug: Ensure all references are assigned
        DebugReferences();

        // Shuffle the questions
        shuffledQuestions = new List<Question>(questions);
        ShuffleList(shuffledQuestions);

        // Add listeners to answer buttons
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int buttonIndex = i; // Capture index for closure
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(buttonIndex));
        }

        // Add listener to skip button
        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(SkipQuestion);

       

        // Display the first question
        DisplayQuestion();
        UpdateScoreText();
        feedbackText.text = ""; // Clear feedback text at the start
    }

    void DebugReferences()
    {
        if (questionImage == null) UnityEngine.Debug.LogError("Question Image is not assigned!");
        if (questionText == null) UnityEngine.Debug.LogError("Question Text is not assigned!");
        if (optionButtons == null || optionButtons.Length == 0) UnityEngine.Debug.LogError("Option Buttons are not assigned!");
        if (scoreText == null) UnityEngine.Debug.LogError("Score Text is not assigned!");
        if (feedbackText == null) UnityEngine.Debug.LogError("Feedback Text is not assigned!");
        if (skipButton == null) UnityEngine.Debug.LogError("Skip Button is not assigned!");
        
    }

    void DisplayQuestion()
    {
        if (currentQuestionIndex >= shuffledQuestions.Count)
        {
            // End of the quiz
            questionImage.gameObject.SetActive(false);
            questionText.text = "Quiz Finished! Final Score: " + score;
            SaveScore(score); //  Save the final score when the quiz ends!

            foreach (Button button in optionButtons)
            {
                button.gameObject.SetActive(false);
            }
            skipButton.gameObject.SetActive(false);
            feedbackText.text = ""; // Clear any feedback
            return;
        }

        // Get the current shuffled question
        Question currentQuestion = shuffledQuestions[currentQuestionIndex];

        // Display the image and text
        questionImage.sprite = currentQuestion.questionImage;
        questionText.text = currentQuestion.questionText;

        // Shuffle the options and display them
        List<int> optionIndices = new List<int> { 0, 1, 2, 3 };
        ShuffleList(optionIndices);

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int shuffledIndex = optionIndices[i];
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.options[shuffledIndex];

            // Check if this button represents the correct answer
            if (shuffledIndex == currentQuestion.correctAnswerIndex)
            {
                optionButtons[i].name = "Correct"; // Mark this button as correct
            }
            else
            {
                optionButtons[i].name = "Wrong"; // Mark this button as wrong
            }
        }

        // Clear feedback
        feedbackText.text = "";
        isFeedbackActive = false;
    }

    void OnOptionSelected(int selectedIndex)
    {
        if (isFeedbackActive) return;

        // Check if the selected answer is correct
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

        UpdateScoreText();

        // Move to the next question after 2 seconds
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
        string newScoreEntry = " Hard Score: " + newScore + " | Date: " + System.DateTime.Now;

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

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

   

    // Helper function to shuffle a list
    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
