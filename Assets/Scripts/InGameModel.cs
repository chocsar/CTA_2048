using UnityEngine;
using System;
using UniRx;

public class InGameModel : MonoBehaviour
{
    public event Action<int[,]> ChangeStageStateEvent;
    public event Action GameOverEvent;
    public event Action<int> ChangeScoreEvent;
    public event Action<int> ChangeHighScoreEvent;

    private StateModel stateModel;
    private ScoreModel scoreModel;

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

    private void ChangeStageStates(int[,] stageState)
    {
        ChangeStageStateEvent?.Invoke(stageState);
    }

    private void GameOver()
    {
        GameOverEvent?.Invoke();
    }

    private void ChangeScore(int score)
    {
        ChangeScoreEvent?.Invoke(score);
    }

    private void ChangeHighScore(int highScore)
    {
        ChangeHighScoreEvent?.Invoke(highScore);
    }


}
