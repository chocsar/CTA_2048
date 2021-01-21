using UnityEngine;
using UnityEngine.UI;
using System;

public class InGameView : MonoBehaviour
{
    private const int StageSize = 4;

    public event Action InputRightEvent;
    public event Action InputLeftEvent;
    public event Action InputUpEvent;
    public event Action InputDownEvent;
    public event Action ClickMenuButtonEvent;

    [SerializeField] private Cell[] cells;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;

    private IInput input;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            input = new SmartphoneInput();
        }
        else
        {
            input = new PCInput();
        }
    }

    private void Update()
    {
        switch (input.GetInput())
        {
            case 1:
                InputRightEvent?.Invoke();
                break;
            case 2:
                InputLeftEvent?.Invoke();
                break;
            case 3:
                InputUpEvent?.Invoke();
                break;
            case 4:
                InputDownEvent?.Invoke();
                break;
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
