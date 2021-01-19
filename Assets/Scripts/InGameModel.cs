﻿using UnityEngine;
using System;

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
        stateModel.ChangeStageStateEvent += ChangeStageState;
        stateModel.GameOverEvent += GameOver;
        stateModel.ChangeScoreEvent += scoreModel.SetScore;

        //ScoreModelの変更を監視する
        scoreModel.ChangeScoreEvent += ChangeScore;
        scoreModel.ChangeHighScoreEvent += ChangeHighScore;
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

    private void ChangeStageState(int[,] stageState)
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
