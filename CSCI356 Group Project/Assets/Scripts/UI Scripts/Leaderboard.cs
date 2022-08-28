using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private float entryTemplateHeight = 35.0f;

    private Transform entryTemplate;

    // Start is called before the first frame update
    void Start()
    {
        entryTemplate = transform.Find("EntryTemplate");

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
        List<Highscore> scoreList = JsonConvert.DeserializeObject<List<Highscore>>(PlayerPrefs.GetString("Leaderboard"));
            
        if (scoreList == null)
        {
            scoreList = new List<Highscore>();
            scoreList.Add(new Highscore("Abc", 0));
            scoreList.Add(new Highscore("Abc", 0));
            scoreList.Add(new Highscore("Abc", 0));
            scoreList.Add(new Highscore("Abc", 0));
            scoreList.Add(new Highscore("Abc", 0));
            scoreList.Add(new Highscore("Abc", 0));
            scoreList.Add(new Highscore("Abc", 0));
            scoreList.Add(new Highscore("Abc", 0));
            scoreList.Add(new Highscore("Abc", 0));
            scoreList.Add(new Highscore("Abc", 0));
        }

        //Debug.Log("Prefs " + PlayerPrefs.GetString("Leaderboard"));

        // Loop to display
        for (int i = 0; i < scoreList.Count; i++)
        {
            Transform entry = Instantiate(entryTemplate, transform);
            RectTransform rectTransform = entry.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, -i * entryTemplateHeight);

            // Activate entry
            entry.gameObject.SetActive(true);

            // Set text to entry
            entry.Find("Rank").GetComponent<TMP_Text>().text = (i + 1).ToString();
            entry.Find("Name").GetComponent<TMP_Text>().text = scoreList[i].Name;
            entry.Find("Score").GetComponent<TMP_Text>().text = scoreList[i].Score.ToString();
        }

    }

    public static void SaveScore()
    {
        List<Highscore> scoreList = JsonConvert.DeserializeObject<List<Highscore>>(PlayerPrefs.GetString("Leaderboard"));

        if (scoreList == null)
        {
            scoreList = new List<Highscore>();
        }

        //Debug.Log("Prefs " + PlayerPrefs.GetString("Leaderboard"));

        // Add new highscore
        scoreList.Add(new Highscore(GameSession.Name, GameSession.Score));

        // Sort descending
        scoreList.Sort((a, b) => b.Score - a.Score);

        // Take only top 10
        scoreList = scoreList.Take(10).ToList();

        // Save to player pref
        PlayerPrefs.SetString("Leaderboard", JsonConvert.SerializeObject(scoreList));
        PlayerPrefs.Save();

        //Debug.Log("Prefs " + PlayerPrefs.GetString("Leaderboard"));
    }

    [JsonObject]
    private class Highscore
    {
        private string name;
        private int score;

        [JsonProperty]
        public string Name { get { return name; } set { name = value; } }

        [JsonProperty]
        public int Score { get { return score; } set { score = value; } }

        public Highscore(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
}
