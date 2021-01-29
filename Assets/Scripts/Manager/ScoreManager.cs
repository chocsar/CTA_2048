using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    /// <param name="score">今のスコア</param>
    public void SaveRanking(int score)
    {
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
