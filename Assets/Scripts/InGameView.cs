using UnityEngine;
using UnityEngine.UI;
using System;

public class InGameView : MonoBehaviour
{
    private const int StageSize = 4;

    public event Action InputRightKeyEvent;
    public event Action InputLeftKeyEvent;
    public event Action InputUpKeyEvent;
    public event Action InputDownKeyEvent;
    public event Action ClickMenuButtonEvent;

    [SerializeField] private Cell[] cells;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;


    private void Update()
    {
        InputKey();
    }

    /// <summary>
    /// ユーザーからのキー入力を受け取る
    /// </summary>
    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //nullチェック
            InputRightKeyEvent?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputLeftKeyEvent?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            InputUpKeyEvent?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            InputDownKeyEvent?.Invoke();
        }
    }

    /// <summary>
    /// ステージの状態を画面上に反映させる
    /// </summary>
    public void ApplyStage(int[,] stageStates)
    {
        for (var row = 0; row < StageSize; row++)
        {
            for (var col = 0; col < StageSize; col++)
            {
                cells[row * StageSize + col].SetText(stageStates[row, col]);
            }
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }
    public void SetHighScore(int score)
    {
        highScoreText.text = $"Score: {score}";
    }

    public void ClickMenuButton()
    {
        ClickMenuButtonEvent?.Invoke();
    }

}
