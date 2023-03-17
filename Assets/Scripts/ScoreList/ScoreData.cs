using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ScoreData
{
    const string fileName = "C:\\Users\\emi_p\\OneDrive\\Desktop\\ScoreData.json";
    public Score scores = new Score();

    public void AddNewScore(string name, int score)
    {
        Score newScore = new Score()
        {
            names = name,
            score = score
        };
        WriteNewScore(newScore);
    }

    public void WriteNewScore(Score newScore)
    {        
        string dataJSON = JsonUtility.ToJson(newScore);
        File.WriteAllText(fileName, dataJSON);
        SceneManager.LoadScene("HighScore");
    }

    public void ReadScore()
    {
       string json = File.ReadAllText(fileName);
        for (int i = 0; i < json.Length; i++)
        {

        }
    }
}
