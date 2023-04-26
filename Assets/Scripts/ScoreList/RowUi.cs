using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowUi : MonoBehaviour
{
    public ScoreManager scoreManager;
    public ScoreUi scoreUiPrefab;

    private void Start()
    {
        for (int i = 0; i < scoreManager.scoreList.Count; i++)
        {
            ScoreUi current = Instantiate(scoreUiPrefab.gameObject, this.transform).GetComponent<ScoreUi>();
            current.nameText.text = scoreManager.scoreList[i].names;
            current.scoreText.text = scoreManager.scoreList[i].score.ToString();
            current.rankText.text = (i+1).ToString();
        }
    }

}
