using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

public class InGameView : MonoBehaviour
{
    private const int StageSize = 4;

    public IObservable<InputDirection> InputEvent
    {
        get { return inputSubject; }
    }
    public IObservable<Unit> ClickMenuButtonEvent
    {
        get { return clickMenuButtonSubject; }
    }

    [SerializeField] private Cell[] cells;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;

    private IInput input;
    private Subject<InputDirection> inputSubject = new Subject<InputDirection>();
    private Subject<Unit> clickMenuButtonSubject = new Subject<Unit>();


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
        InputDirection inputDirection = input.GetInput();
        
        if(inputDirection != InputDirection.None)
        {
            inputSubject.OnNext(inputDirection);
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
        clickMenuButtonSubject.OnNext(Unit.Default);
    }

}
