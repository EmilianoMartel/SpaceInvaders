using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [System.Serializable]
    public class Score
    {
        public string names;
        public float score;
    }

    public TMPro.TMP_InputField NameInput;
    public TMPro.TMP_Text nameText;
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text scoreTextData;
    public TMPro.TMP_Text rankText;
    public Score score = new Score();
    public ScoreData scoreData;
    public List<Score> scoreList = new List<Score>();
    const string FILENAME = "/ScoreData.json";


    public void Awake()
    {
        //If the file dont exist create file and format
        if (!File.Exists(Application.dataPath + FILENAME))
        {
            List<Score> scoreList = new List<Score>();
            //create the struct
            for (int i = 0; i < 9; i++)
            {
                score.names = "AAA";
                score.score = 0f;
                scoreList.Add(score);
            }
            string strOutput = JsonConvert.SerializeObject(scoreList, Formatting.Indented);
            File.WriteAllText(Application.dataPath + FILENAME, strOutput);
        }
        else //if exist put data in list scoreList
        {
            scoreList = JsonConvert.DeserializeObject<List<Score>>(File.ReadAllText(Application.dataPath + FILENAME));
        }
        if (scoreTextData.IsActive())
        {
            scoreTextData.text = scoreData.score.ToString();
        }
    }

    public void AddNewScore()
    {
        //Collect data from input
        score.names = NameInput.text;
        score.score = scoreData.score;
        scoreList.Add(score);
        //Order score major to minor
        scoreList.Sort((x, y) => y.score.CompareTo(x.score));
        //remove the last score if the list is more 10 elements
        if (scoreList.Count > 10)
        {
            do
            {
                scoreList.RemoveRange(scoreList.Count - 1, 1);
            } while (scoreList.Count > 10);
        }
        string strOutput = JsonConvert.SerializeObject(scoreList, Formatting.Indented);
        File.WriteAllText(Application.dataPath + FILENAME, strOutput);
        SceneManager.LoadScene("HighScore");
    }
}
