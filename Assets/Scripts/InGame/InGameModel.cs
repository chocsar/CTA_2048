using UnityEngine;
using System;
using UniRx;

public class InGameModel : MonoBehaviour
{
    public IObservable<int[,]> ChangeStageStatesEvent
    {
        get { return stageStatesSubject; }
    }
    public IObservable<int> ChangeScoreEvent
    {
        get { return scoreSubject; }
    }
    public IObservable<int> ChangeHighScoreEvent
    {
        get { return highScoreSubject; }
    }
    public IObservable<Unit> GameOverEvent
    {
        get { return gameOverSubject; }
    }

    private StateModel stateModel;
    private ScoreModel scoreModel;

    private Subject<int[,]> stageStatesSubject = new Subject<int[,]>();
    private Subject<int> scoreSubject = new Subject<int>();
    private Subject<int> highScoreSubject = new Subject<int>();
    private Subject<Unit> gameOverSubject = new Subject<Unit>();

    private void Awake()
    {
        stateModel = GetComponent<StateModel>();
        scoreModel = GetComponent<ScoreModel>();

        //StateModelの変更を監視する
        stateModel.ChangeStageStatesEvent.Subscribe(ChangeStageStates);
        stateModel.ChangeScoreEvent.Subscribe(scoreModel.SetScore);
        stateModel.GameOverEvent.Subscribe(_ => GameOver());

        //ScoreModelの変更を監視する
        scoreModel.ChangeScoreEvent.Subscribe(ChangeScore);
        scoreModel.ChangeHighScoreEvent.Subscribe(ChangeHighScore);
    }

    public void InitStage()
    {
        stateModel.InitStage();
    }

    public void MoveCells(InputDirection inputDirection)
    {
        stateModel.MoveCells(inputDirection);
    }

    public int GetScore()
    {
        return scoreModel.GetScore();
    }

    public void ResetScore()
    {
        scoreModel.ResetScore();
    }

    public void SetHighScore(int highScore)
    {
        scoreModel.SetHighScore(highScore);
    }

    public int GetHighScore()
    {
        return scoreModel.GetHighScore();
    }

    public void SaveScore(int score)
    {
        scoreModel.SaveScore(score);
    }

    public void SaveHighScore()
    {
        scoreModel.SaveHighScore();
    }

    public int LoadHighScore()
    {
        return scoreModel.LoadHighScore();
    }

    public void SaveRanking()
    {
        scoreModel.SaveRanking();
    }

    private void ChangeStageStates(int[,] stageState)
    {
        stageStatesSubject.OnNext(stageState);
    }

    private void ChangeScore(int score)
    {
        scoreSubject.OnNext(score);
    }

    private void ChangeHighScore(int highScore)
    {
        highScoreSubject.OnNext(highScore);
    }

    private void GameOver()
    {
        gameOverSubject.OnNext(Unit.Default);
    }


}
