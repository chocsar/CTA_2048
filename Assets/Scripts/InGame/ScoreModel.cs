using System;
using UnityEngine;
using System.Collections.Generic;
using UniRx;
using System.IO;

public class ScoreModel : MonoBehaviour
{
    public IObservable<int> ChangeScoreEvent
    {
        get { return score; }
    }
    public IObservable<int> ChangeHighScoreEvent
    {
        get { return highScore; }
    }

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
        string filePath = Application.dataPath + FilePath.ScoreRanking;

        //ファイルからロード
        using (StreamReader reader = new StreamReader(filePath))
        {
            if (reader.Peek() == -1)
            {
                return 0;
            }
            else
            {
                //一行目に書かれた値を返す
                return int.Parse(reader.ReadLine());
            }
        }
    }

    /// <summary>
    /// スコアをランキング形式でファイルに保存する
    /// </summary>
    public void SaveRanking()
    {
        int score = GetScore();

        string filePath = Application.dataPath + FilePath.ScoreRanking;
        bool isAppend = false;
        string allText = null;

        //ファイルのロード
        using (StreamReader reader = new StreamReader(filePath))
        {
            //ファイルにデータがない場合
            if (reader.Peek() == -1)
            {
                allText += score.ToString() + "\n";
            }
            else
            {
                bool isWritten = false;

                while (reader.Peek() != -1)
                {
                    string line = reader.ReadLine();

                    //今回のスコアを追加
                    if (int.Parse(line) < score && !isWritten)
                    {
                        isWritten = true;
                        allText += score.ToString() + "\n";
                    }

                    allText += line + "\n";
                }
            }
        }
        //ファイルのセーブ
        using (StreamWriter writer = new StreamWriter(filePath, isAppend))
        {
            writer.Write(allText);
        }
    }

    /// <summary>
    /// ランキング順のスコアリストを返す
    /// </summary>
    /// <returns>ランキング順のスコアリスト</returns>
    public List<int> LoadRanking()
    {
        List<int> scoreList = new List<int>();
        string filePath = Application.dataPath + FilePath.ScoreRanking;

        //ファイルのロード
        using (StreamReader reader = new StreamReader(filePath))
        {
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                scoreList.Add(int.Parse(line));
            }
        }

        return scoreList;
    }

}
