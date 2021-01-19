using System;
using UnityEngine;

public class ScoreModel : MonoBehaviour
{
    public event Action<int> ChangeScoreEvent;
    public event Action<int> ChangeHighScoreEvent;
    private int score;
    private int highScore;

    /// <summary>
    /// スコアの計算ロジック
    /// </summary>
    /// <param name="cellValue">合成する数値マスの値</param>
    public void SetScore(int cellValue)
    {
        score += cellValue * 2;
        ChangeScoreEvent?.Invoke(score);

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
        ChangeScoreEvent?.Invoke(score);
    }

    /// <summary>
    /// ハイスコアをセットする
    /// </summary>
    /// <param name="score">スコア</param>
    public void SetHighScore(int score)
    {
        highScore = score;
        ChangeHighScoreEvent?.Invoke(highScore);
    }

    /// <summary>
    /// ハイスコアを取得する
    /// </summary>
    /// <returns>ハイスコア</returns>
    public int GetHighScore()
    {
        return highScore;
    }

    /// <summary>
    /// スコアをセーブする
    /// </summary>
    /// <param name="score">スコア</param>
    public void SaveScore(int score)
    {
        ScoreManager.Instance.SaveScore(score);
    }

    /// <summary>
    /// ハイスコアをセーブする
    /// </summary>
    public void SaveHighScore()
    {
        int score = GetScore();
        int highScore = GetHighScore();

        if (score == highScore)
        {
            ScoreManager.Instance.SaveHighScore(highScore);
        }
    }

    /// <summary>
    /// ハイスコアをロードする
    /// </summary>
    /// <returns>ハイスコア</returns>
    public int LoadHighScore()
    {
        return ScoreManager.Instance.LoadHighScore();
    }
}
