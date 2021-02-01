using System;
using UnityEngine;
using UniRx;

public class ScoreModel : MonoBehaviour
{
    public IObservable<int> ChangeScoreEvent => score;
    public IObservable<int> ChangeHighScoreEvent => highScore;

    private ReactiveProperty<int> score = new ReactiveProperty<int>();
    private ReactiveProperty<int> highScore = new ReactiveProperty<int>();

    /// <summary>
    /// スコアの計算ロジック
    /// </summary>
    /// <param name="cellValue">合成する数値マスの値</param>
    public void SetScore(int cellValue)
    {

        score.Value += cellValue * 2;

        //ハイスコア更新
        if (score.Value > highScore.Value)
        {
            SetHighScore(score.Value);
        }
    }

    /// <summary>
    /// スコアを取得する
    /// </summary>
    /// <returns>スコア</returns>
    public int GetScore()
    {
        return score.Value;
    }

    /// <summary>
    /// スコアを0にする
    /// </summary>
    public void ResetScore()
    {
        score.Value = 0;
    }

    /// <summary>
    /// ハイスコアをセットする
    /// </summary>
    /// <param name="score">スコア</param>
    public void SetHighScore(int score)
    {
        highScore.Value = score;
    }

    /// <summary>
    /// ハイスコアを取得する
    /// </summary>
    /// <returns>ハイスコア</returns>
    public int GetHighScore()
    {
        return highScore.Value;
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
