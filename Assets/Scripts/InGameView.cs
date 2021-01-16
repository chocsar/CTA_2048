using UnityEngine;
using UnityEngine.UI;
using System;

public class InGameView : MonoBehaviour
{
    private const int StageSize = 4;

    public event Action InputRightKey;
    public event Action InputLeftKey;
    public event Action InputUpKey;
    public event Action InputDownKey;
    public event Action ClickMenuButton;

    [SerializeField] private Cell[] cells;
    [SerializeField] private Text scoreText;


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
            InputRightKey?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputLeftKey?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            InputUpKey?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            InputDownKey?.Invoke();
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

}
