using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using Newtonsoft.Json;

public class ScoreManager : MonoBehaviour
{
    [System.Serializable]
    public class Score
    {
        public string names;
        public float score;
    }

    public TMPro.TMP_InputField NameInput;
    public TMPro.TMP_InputField ScoreInput;
    public Score score = new Score();
    public List<Score> scoreList = new List<Score>();
    const string FILENAME = "/ScoreData.json";

    public void AddNewScore()
    {
        //Collect data from input
        score.names = NameInput.text;
        score.score = float.Parse(ScoreInput.text);
        //If the file dont exist
        if (!File.Exists(Application.dataPath + FILENAME)) 
        {
            List<Score> newScoreList = new List<Score>();
            Score newScore = new Score();
            newScore.names = NameInput.text;
            newScore.score= float.Parse(ScoreInput.text);
            //create the struct
            for (int i = 0; i < 9; i++)
            {
                score.names = "AAA";
                score.score = 0f;
                newScoreList.Add(score);
            }
            newScoreList.Add(newScore);
            //order list minor to major
            newScoreList.Sort((x,y) => x.score.CompareTo(y.score));
            //reverse
            newScoreList.Reverse();
            //save
            string strOutput = JsonConvert.SerializeObject(newScoreList, Formatting.Indented);
            File.WriteAllText(Application.dataPath + FILENAME, strOutput);
        }
        else
        {
            scoreList = JsonConvert.DeserializeObject<List<Score>>(File.ReadAllText(Application.dataPath + FILENAME));
            scoreList.Add(score);
            scoreList.Sort((x, y) => x.score.CompareTo(y.score));
            scoreList.Reverse();
            //remove the last score if the list is more 10 elements
            if (scoreList.Count > 10)
            {
                do
                {
                    scoreList.RemoveRange(scoreList.Count-1, 1);
                }while(scoreList.Count > 10);
            }
            string strOutput = JsonConvert.SerializeObject(scoreList, Formatting.Indented);
            File.WriteAllText(Application.dataPath + FILENAME, strOutput);
        }
    }
}
