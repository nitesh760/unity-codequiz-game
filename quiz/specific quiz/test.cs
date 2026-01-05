/*
NAME: Nitesh Suresh Bhalekar
PROJECT: Code Quiz Game 
TYBSC-IT ROLL NUMBER -> 706 
*/
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class test : MonoBehaviour
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
    public TextMeshProUGUI timerText;
    public Button skipButton;
    public List<Question> questions;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private float timeRemaining = 5f;
    private bool isTimerRunning = false;
    private List<Question> shuffledQuestions;
    private Coroutine timerCoroutine;

    void Start()
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
    }
    //debug show in unity console
    void DebugReference()
    {
        if (questionText == null) UnityEngine.Debug.LogError("Question Text is not assigned!");
        if (optionButtons == null || optionButtons.Length == 0) UnityEngine.Debug.LogError("Option Buttons are not assigned!");
        if (scoreText == null) UnityEngine.Debug.LogError("Score Text is not assigned!");
        if (timerText == null) UnityEngine.Debug.LogError("Timer Text is not assigned!");
        if (skipButton == null) UnityEngine.Debug.LogError("Skip Button is not assigned!");
    }
    //displays question
    void DisplayQuestion()
    {
        if (currentQuestionIndex >= shuffledQuestions.Count)
        {
            string finalMessage = "Quiz Finished! Final Score: " + score;
            if (score >= 50)
                finalMessage += "\nExcellent!";
            else if (score >= 40)
                finalMessage += "\nVery Good!";
            else if (score >= 30)
                finalMessage += "\nGood!";
            else if (score >= 30)
                finalMessage += "\nNeed more practice!";
            else if (score < 10)
                finalMessage += "\nBad Practice more and improve yourself!";
            questionText.text = finalMessage;
            SaveScore(score);
            foreach (Button button in optionButtons)
            {
                button.gameObject.SetActive(false);
            }
            skipButton.gameObject.SetActive(false);
            timerText.text = "";
            isTimerRunning = false;
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }
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
        StartTimer();
    }
    //opionselection
    void OnOptionSelected(int selectedIndex)
    {
        if (!isTimerRunning) return;
        Button selectedButton = optionButtons[selectedIndex];
        if (selectedButton.name == "Correct")
        {
            score++;
        }
        UpdateScore();
        NextQuestion();
    }
    //nextquestion
    void NextQuestion()
    {
        currentQuestionIndex++;
        DisplayQuestion();
    }
    //skip
    void SkipQuestion()
    {
        if (!isTimerRunning) return;
        NextQuestion();
    }
    //final score 
    void UpdateScore()
    {
        if (currentQuestionIndex >= shuffledQuestions.Count)
        {
            scoreText.text = "Final Score: " + score;
        }
        else
        {
            scoreText.text = "";
        }
    }
    //save the score in scroll view
    void SaveScore(int newScore)
    {
        string newScoreEntry = " Test Score: " + newScore + " | Date: " + System.DateTime.Now;
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
    //shuffle questions
    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    //timer
    void StartTimer()
    {
        if (currentQuestionIndex >= shuffledQuestions.Count)
        {
            isTimerRunning = false;
            timerText.text = "";
            return;
        }
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        timeRemaining = 5f;
        isTimerRunning = true;
        timerCoroutine = StartCoroutine(TimerCountdown());
    }
    //time display
    IEnumerator TimerCountdown()
    {
        while (timeRemaining > 0)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining);
            yield return new WaitForSeconds(1f);
            timeRemaining--;
        }
        isTimerRunning = false;
        timerText.text = "Time's up!";
        yield return new WaitForSeconds(1f);
        NextQuestion();
    }
}
