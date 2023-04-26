using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData:MonoBehaviour
{
    public float score;

    public void ClearScore()
    {
        score = 0;
    }

    public void IncrementScore(float increment)
    {
        score += increment;
    }
}
