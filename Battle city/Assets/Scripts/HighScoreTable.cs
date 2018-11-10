using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour {
    ScoreCounter ScoreCounter;
    public Text Label;
    private int minLenght;
    private int scoreCount = 10;
	// Use this for initialization
	public void GetScore () {
        int childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }

        ScoreCounter = GameObject.Find("Score").GetComponent<ScoreCounter>();

        var descendHighScores = ScoreCounter.HighScores.OrderByDescending(d => d).ToArray();

        if (descendHighScores.Length > scoreCount)
        {
            minLenght = scoreCount;
        }
        else
        {
            minLenght = descendHighScores.Length;
        }

        for (int i = 0; i < minLenght; i++)
        {
            var rank = i + 1;
            var curLabel = Instantiate(Label, gameObject.transform);
            curLabel.text += rank + " : " + descendHighScores[i] + "\n";
            curLabel.transform.SetParent(gameObject.transform);
        }
	}
}
