using UnityEngine;
using System;

public class InGameModel : MonoBehaviour
{
    public event Action<int[,]> ChangeStageState;
    public event Action GameOver;
    public event Action<int> ChangeScore;
    public event Action<int> ChangeHighScore;

    private StateModel stateModel;
    private ScoreModel scoreModel;

    private void Awake()
    {
        stateModel = GetComponent<StateModel>();
        scoreModel = GetComponent<ScoreModel>();

        //StateModelの変更を監視する
        stateModel.ChangeStageState += OnChangeStageState;
        stateModel.GameOver += OnGameOver;
        stateModel.ChangeScore += scoreModel.SetScore;

        //ScoreModelの変更を監視する
        scoreModel.ChangeScore += OnChangeScore;
        scoreModel.ChangeHighScore += OnChangeHighScore;
    }

    public void InitStage()
    {
        stateModel.InitStage();
    }

    public void MoveCellRight()
    {
        stateModel.MoveCellRight();
    }

    public void MoveCellLeft()
    {
        stateModel.MoveCellLeft();
    }

    public void MoveCellUp()
    {
        stateModel.MoveCellUp();
    }

    public void MoveCellDown()
    {
        stateModel.MoveCellDown();
    }

    public void SetScore(int score)
    {
        scoreModel.SetScore(score);
    }

    public int GetScore()
    {
        return scoreModel.GetScore();
    }

    public void ResetScore()
    {
        scoreModel.ResetScore();
    }

    public void SetHighScore(int score)
    {
        scoreModel.SetHighScore(score);
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

    private void OnChangeStageState(int[,] stageState)
    {
        ChangeStageState?.Invoke(stageState);
    }

    private void OnGameOver()
    {
        GameOver?.Invoke();
    }

    private void OnChangeScore(int score)
    {
        ChangeScore?.Invoke(score);
    }

    private void OnChangeHighScore(int highScore)
    {
        ChangeHighScore?.Invoke(highScore);
    }


}
