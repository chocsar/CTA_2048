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
}
