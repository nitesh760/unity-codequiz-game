using UnityEngine;
using System.Collections.Generic;
using System;
using System.Diagnostics;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private List<string> scores = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveScore(int score)
    {
        List<string> scores = GetSavedScores();

        string scoreEntry = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - Score: " + score;
        scores.Add(scoreEntry);

        PlayerPrefs.SetString("SavedScores", string.Join("|", scores));
        PlayerPrefs.Save();

        UnityEngine.Debug.Log("Saved Score: " + scoreEntry);
    }


    public List<string> GetSavedScores()
    {
        string savedScores = PlayerPrefs.GetString("SavedScores", "");
        scores = new List<string>(savedScores.Split('|'));
        return scores;
    }
}
