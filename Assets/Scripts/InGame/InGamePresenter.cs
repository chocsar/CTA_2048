using UnityEngine;
using UniRx;
using System;

public class InGamePresenter : MonoBehaviour
{
    public IObservable<Unit> rankingWindowEvent
    {
        get { return rankingWindowSubject; }
    }

    private InGameModel inGameModel;
    private InGameView inGameView;
    [SerializeField] private MenuWindowView menuWindowView;
    [SerializeField] private RankingWindowPresenter rankingWindowPresenter;

    private Subject<Unit> rankingWindowSubject = new Subject<Unit>();

    private void Awake()
    {
        inGameModel = GetComponent<InGameModel>();
        inGameView = GetComponent<InGameView>();

        // Modelの値の変更を監視する
        inGameModel.ChangeStageStatesEvent.Subscribe(inGameView.ApplyStage);
        inGameModel.ChangeScoreEvent.Subscribe(inGameView.SetScore);
        inGameModel.ChangeHighScoreEvent.Subscribe(inGameView.SetHighScore);
        inGameModel.GameOverEvent.Subscribe(_ => GameOver());

        // Viewの入力を監視する
        inGameView.InputEvent.Subscribe(MoveCells);
        inGameView.ClickMenuButtonEvent.Subscribe(_ => menuWindowView.OpenWindow());
        menuWindowView.ClickRestartButtonEvent.Subscribe(_ => RestartGame());
        inGameView.ClickRankingButtonEvent.Subscribe(_ => rankingWindowPresenter.OpenWindow());

    }

    private void Start()
    {
        // 初期化
        inGameModel.InitStage();
        inGameModel.SetHighScore(inGameModel.LoadHighScore());
        inGameModel.ResetScore();
    }

    private void MoveCells(InputDirection inputDirection)
    {
        //メニューが開いてる場合は移動させない
        if (menuWindowView.IsOpenWindow()) return;

        inGameModel.MoveCells(inputDirection);
    }

    /// <summary>
    /// ゲームオーバー時に行う処理
    /// </summary>
    private void GameOver()
    {
        inGameModel.SaveRanking();
        inGameModel.SaveScore(inGameModel.GetScore());
        LoadResultScene();
    }

    /// <summary>
    /// ゲームをリスタートする
    /// </summary>
    private void RestartGame()
    {
        inGameModel.SaveRanking();

        inGameModel.InitStage();
        inGameModel.SetHighScore(inGameModel.LoadHighScore());
        inGameModel.ResetScore();
    }

    /// <summary>
    /// リザルトシーンへ遷移する
    /// </summary>
    private void LoadResultScene()
    {
        SceneController.Instance.LoadScene(SceneNames.Result);
    }


}