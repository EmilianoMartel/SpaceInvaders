using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ScoreData
{
    const string fileName = "C:\\Users\\emi_p\\OneDrive\\Desktop\\ScoreData.json";
    public Score scores = JsonConvert.DeserializeObject<Score>(File.ReadAllText(fileName));
    private List<Score> scoresList = new List<Score>();

    public void AddNewScore(string name, int score)
    {
        Score newScore = new Score()
        {
            names = name,
            score = score
        };
        scoresList.Add(newScore);

        WriteNewScore(scoresList);
    }

    //revisar
    public void WriteNewScore(List<Score> newScore)
    {
        if(File.Exists(fileName))
        {            
            string json = JsonUtility.ToJson(newScore);
            Debug.Log(json);
            Debug.Log(scores);
            File.WriteAllText(fileName, json);
        }
        else
        {
            string dataJSON = JsonConvert.SerializeObject(newScore);
            File.WriteAllText(fileName, dataJSON);
            SceneManager.LoadScene("HighScore");
        }
    }

    public void ReadScore()
    {
        Dictionary<string, string> nameDict = new Dictionary<string, string>();
    }
}
