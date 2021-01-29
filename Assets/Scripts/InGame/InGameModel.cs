using UnityEngine;
using System;
using UniRx;

public class InGameModel : MonoBehaviour
{
    public IObservable<int[,]> ChangeStageStatesEvent => stateModel.ChangeStageStatesEvent;
    public IObservable<Unit> GameOverEvent => stateModel.GameOverEvent;
    public IObservable<int> ChangeScoreEvent => scoreModel.ChangeScoreEvent;
    public IObservable<int> ChangeHighScoreEvent => scoreModel.ChangeHighScoreEvent;

    private StateModel stateModel;
    private ScoreModel scoreModel;

    public void Initialize()
    {
        stateModel = GetComponent<StateModel>();
        scoreModel = GetComponent<ScoreModel>();

        //StateModelの変更を監視する
        stateModel.MergeCellsEvent.Subscribe(scoreModel.SetScore);
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

    public int LoadHighScore()
    {
        return scoreModel.LoadHighScore();
    }

    public void SaveRanking(int score)
    {
        scoreModel.SaveRanking(score);
    }

}
