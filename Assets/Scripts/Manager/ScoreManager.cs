using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    public void SaveScore(int score)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.Score, score);
    }

    public int LoadScore()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.Score, 0);
    }

    public void SaveHighScore(int highScore)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.HighScore, highScore);
    }

    public int LoadHighScore()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.HighScore, 0);
    }
}
