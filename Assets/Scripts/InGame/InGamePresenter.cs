using UnityEngine;
using UniRx;

public class InGamePresenter : MonoBehaviour
{
    private InGameModel inGameModel;
    private InGameView inGameView;
    [SerializeField] private MenuWindowView menuWindowView;

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
        inGameModel.SaveHighScore();
        inGameModel.SaveScore(inGameModel.GetScore());
        LoadResultScene();
    }

    /// <summary>
    /// ゲームをリスタートする
    /// </summary>
    private void RestartGame()
    {
        inGameModel.SaveHighScore();

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