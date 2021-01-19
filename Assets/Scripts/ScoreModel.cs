using System;
using UnityEngine;

public class ScoreModel : MonoBehaviour
{
    public event Action<int> ChangeScore;
    public event Action<int> ChangeHighScore;
    private int score;
    private int highScore;

    /// <summary>
    /// スコアの計算ロジック
    /// </summary>
    /// <param name="cellValue">合成する数値マスの値</param>
    public void SetScore(int cellValue)
    {
        score += cellValue * 2;
        ChangeScore?.Invoke(score);

        //ハイスコア更新
        if (score > highScore)
        {
            SetHighScore(score);
        }
    }

    /// <summary>
    /// スコアを取得する
    /// </summary>
    /// <returns>スコア</returns>
    public int GetScore()
    {
        return score;
    }

    /// <summary>
    /// スコアを0にする
    /// </summary>
    public void ResetScore()
    {
        score = 0;
        ChangeScore?.Invoke(score);
    }

    /// <summary>
    /// ハイスコアをセットする
    /// </summary>
    /// <param name="score">スコア</param>
    public void SetHighScore(int score)
    {
        highScore = score;
        ChangeHighScore?.Invoke(highScore);
    }

    /// <summary>
    /// ハイスコアを取得する
    /// </summary>
    /// <returns>ハイスコア</returns>
    public int GetHighScore()
    {
        return highScore;
    }
}
