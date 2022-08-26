using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private float entryTemplateHeight = 35.0f;

    //private Transform container;
    private Transform entryTemplate;

    // Start is called before the first frame update
    void Start()
    {
        //container = transform.Find("EntryContainer");
        entryTemplate = transform.Find("EntryTemplate");

        //Debug.Log(container == null ? "Container is null" : "Container not null");
        //Debug.Log(entryTemplate == null ? "Template is null" : "Template not null");

        // Deactivate template
        entryTemplate.gameObject.SetActive(false);

        DisplayLeaderboard();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void DisplayLeaderboard()
    {
        HighscoreList highscoreList = JsonUtility.FromJson<HighscoreList>(PlayerPrefs.GetString("Leaderboard"));

        if (highscoreList == null || highscoreList.scoreList == null)
        {
            highscoreList = new HighscoreList();
        }

        //// Sort descending
        //highscoreList.scoreList.Sort((a, b) => b.Score - a.Score);

        //// Take only top 10
        //highscoreList.scoreList.Take(10);

        // Loop to display
        for (int i = 0; i < highscoreList.scoreList.Count; i++)
        {
            Transform entry = Instantiate(entryTemplate, transform);
            RectTransform rectTransform = entry.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, -i * entryTemplateHeight);

            // Activate entry
            entry.gameObject.SetActive(true);

            // Set text to entry
            entry.Find("Rank").GetComponent<TMP_Text>().SetText((i + 1) + "");
            entry.Find("Name").GetComponent<TMP_Text>().text = highscoreList.scoreList[i].Name;
            entry.Find("Score").GetComponent<TMP_Text>().text = highscoreList.scoreList[i].Score + "";
        }
    }

    public static void SaveScore()
    {
        HighscoreList highscoreList = JsonUtility.FromJson<HighscoreList>(PlayerPrefs.GetString("Leaderboard"));

        if (highscoreList == null || highscoreList.scoreList == null)
        {
            highscoreList = new HighscoreList();
        }

        // Add new highscore
        highscoreList.scoreList.Add(new Highscore(GameSession.Name, GameSession.Score));

        // Sort descending
        highscoreList.scoreList.Sort((a, b) => b.Score - a.Score);

        // Take only top 10
        highscoreList.scoreList.Take(10);

        //Debug.Log("Serialize " + JsonUtility.ToJson(highscoreList));

        // Save to player pref
        PlayerPrefs.SetString("Leaderboard", JsonUtility.ToJson(highscoreList));
        PlayerPrefs.Save();

        //Debug.Log("Prefs " + PlayerPrefs.GetString("Leaderboard"));
    }

    private class HighscoreList
    {
        public List<Highscore> scoreList;

        public HighscoreList()
        {
            scoreList = new List<Highscore>();
        }
    }

    [System.Serializable]
    private class Highscore
    {
        public string Name;
        public int Score;

        public Highscore(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}
