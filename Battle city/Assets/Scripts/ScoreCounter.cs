using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    private int Score = 0;
    private int ScoreMultiplayer = 90;
    private int ScoreBonus = 10;
    public int[] HighScores;

    private void Start()
    {
        HighScores = PlayerPrefsX.GetIntArray("HighScores");
    }

    public int GetRoundScore(int maxEnemyCount, int levelNumber)
    {
        Score += maxEnemyCount * ((levelNumber * ScoreBonus) + ScoreMultiplayer);
        return Score;
    }

    public int GetFinaleScore(int maxEnemyCount, int levelNumber)
    {
        Score += (maxEnemyCount - EnemyHealth.EnemyCount) * ((levelNumber * ScoreBonus) + ScoreMultiplayer);
        return Score;
    }

    public void SaveScore()
    {
        List<int> newScores = new List<int>(HighScores);
        newScores.Add(Score);
        HighScores = newScores.ToArray();
        PlayerPrefsX.SetIntArray("HighScores", HighScores);
        Score = 0;
    }
}
