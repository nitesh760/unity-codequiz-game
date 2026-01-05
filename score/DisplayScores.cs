using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Diagnostics;

public class DisplayScores : MonoBehaviour
{
    public RectTransform content; // The Scroll View Content
    public TextMeshProUGUI scoreEntryTemplate; // The existing Text (TMP) inside Content

    void Start()
    {
        LoadScores();
    }

    void LoadScores()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        string savedScores = PlayerPrefs.GetString("SavedScores", "");
        if (string.IsNullOrEmpty(savedScores))
        {
            UnityEngine.Debug.Log("No Score Found");
            return;
        }

        string[] scoreEntries = savedScores.Split('|');
        foreach (string scoreData in scoreEntries)
        {
            if (!string.IsNullOrWhiteSpace(scoreData))
            {
                GameObject newTextObj = new GameObject("ScoreEntry");
                newTextObj.transform.SetParent(content.transform, false);

                TextMeshProUGUI scoreEntry = newTextObj.AddComponent<TextMeshProUGUI>();
                scoreEntry.text = scoreData;
                scoreEntry.fontSize = 40;
                scoreEntry.color = Color.red;

                RectTransform rectTransform = newTextObj.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(0, 50);
            }
        }
    }



    List<string> GetSavedScores()
    {
        List<string> scores = new List<string>();
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);

        for (int i = 0; i < scoreCount; i++)
        {
            scores.Add(PlayerPrefs.GetString("Score_" + i, "No Data"));
        }

        return scores;
    }
}
