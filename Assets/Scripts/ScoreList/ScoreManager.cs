using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMPro.TMP_InputField NameInput;
    public TMPro.TMP_InputField ScoreInput;
    public ScoreData ScoreData;

    public void Start()
    {
        ScoreData = new ScoreData();
    }

    public void AddNewScore()
    {
        ScoreData.AddNewScore(NameInput.text, Convert.ToInt32(ScoreInput.text));
    }
}
