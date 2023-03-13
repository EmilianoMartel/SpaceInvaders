using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class ScoreData
{
    public List<Score> scores;
    const string fileName = "C:\\Users\\emi_p\\OneDrive\\Desktop\\ScoreData.txt";

    public ScoreData() 
    {
        scores = new List<Score>();
    }

    public void AddNewScore(string name, int score)
    {
        scores.Add(new Score(name, score));
        WriteNewScore();
    }

    public void WriteNewScore()
    {
        using (var stream = File.Open(fileName, FileMode.Create))
        {
            using (var writer = new StreamWriter(stream))
            {
                for (int i = 0; i < scores.Count; i++)
                {
                    writer.Write(scores[i].names);
                    writer.Write(scores[i].score);
                }
            }
        }
    }
}
