using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

public class InGameView : MonoBehaviour
{
    private const int StageSize = 4;

    public IObservable<Unit> InputRightEvent
    {
        get { return inputRightSubject; }
    }
    public IObservable<Unit> InputLeftEvent
    {
        get { return inputLeftSubject; }
    }
    public IObservable<Unit> InputUpEvent
    {
        get { return inputUpSubject; }
    }
    public IObservable<Unit> InputDownEvent
    {
        get { return inputDownSubject; }
    }

    public event Action ClickMenuButtonEvent;

    [SerializeField] private Cell[] cells;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;

    private IInput input;
    private Subject<Unit> inputRightSubject = new Subject<Unit>();
    private Subject<Unit> inputLeftSubject = new Subject<Unit>();
    private Subject<Unit> inputUpSubject = new Subject<Unit>();
    private Subject<Unit> inputDownSubject = new Subject<Unit>();


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
            case InputDirection.Right:
                inputRightSubject.OnNext(Unit.Default);
                break;
            case InputDirection.Left:
                inputLeftSubject.OnNext(Unit.Default);
                break;
            case InputDirection.Up:
                inputUpSubject.OnNext(Unit.Default);
                break;
            case InputDirection.Down:
                inputDownSubject.OnNext(Unit.Default);
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
