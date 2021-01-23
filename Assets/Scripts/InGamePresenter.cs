using UnityEngine;

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
        inGameModel.ChangeStageStateEvent += inGameView.ApplyStage;
        inGameModel.GameOverEvent += GameOver;
        inGameModel.ChangeScoreEvent += inGameView.SetScore;
        inGameModel.ChangeHighScoreEvent += inGameView.SetHighScore;

        // Viewの入力を監視する
        inGameView.InputRightEvent += MoveCellsRight;
        inGameView.InputLeftEvent += MoveCellsLeft;
        inGameView.InputUpEvent += MoveCellsUp;
        inGameView.InputDownEvent += MoveCellsDown;
        inGameView.ClickMenuButtonEvent += menuWindowView.OpenWindow;
        menuWindowView.ClickRestartButtonEvent += RestartGame;

    }

    private void Start()
    {
        // 初期化
        inGameModel.InitStage();
        inGameModel.SetHighScore(inGameModel.LoadHighScore());
        inGameModel.ResetScore();
    }

    private void MoveCellsRight()
    {
        if (menuWindowView.IsOpenWindow()) return;
        inGameModel.MoveCellsRight();
    }
    private void MoveCellsLeft()
    {
        if (menuWindowView.IsOpenWindow()) return;
        inGameModel.MoveCellsLeft();
    }
    private void MoveCellsUp()
    {
        if (menuWindowView.IsOpenWindow()) return;
        inGameModel.MoveCellsUp();
    }
    private void MoveCellsDown()
    {
        if (menuWindowView.IsOpenWindow()) return;
        inGameModel.MoveCellsDown();
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
        menuWindowView.CloseWindow();
    }

    /// <summary>
    /// リザルトシーンへ遷移する
    /// </summary>
    private void LoadResultScene()
    {
        SceneController.Instance.LoadScene(SceneNames.Result);
    }


}