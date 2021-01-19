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
        inGameModel.ChangeStageState += inGameView.ApplyStage;
        inGameModel.GameOver += GameOver;
        inGameModel.ChangeScore += inGameView.SetScore;
        inGameModel.ChangeHighScore += inGameView.SetHighScore;

        // Viewの入力を監視する
        inGameView.InputRightKey += MoveCellRight;
        inGameView.InputLeftKey += MoveCellLeft;
        inGameView.InputUpKey += MoveCellUp;
        inGameView.InputDownKey += MoveCellDown;
        inGameView.OnClickMenuButton += menuWindowView.OpenWindow;
        menuWindowView.OnClickRestartButton += RestartGame;

    }

    private void Start()
    {
        // 初期化
        inGameModel.InitStage();
        inGameModel.SetHighScore(inGameModel.LoadHighScore());
        inGameModel.ResetScore();
    }

    private void MoveCellRight()
    {
        if (menuWindowView.IsOpenWindow()) return;
        inGameModel.MoveCellRight();
    }
    private void MoveCellLeft()
    {
        if (menuWindowView.IsOpenWindow()) return;
        inGameModel.MoveCellLeft();
    }
    private void MoveCellUp()
    {
        if (menuWindowView.IsOpenWindow()) return;
        inGameModel.MoveCellUp();
    }
    private void MoveCellDown()
    {
        if (menuWindowView.IsOpenWindow()) return;
        inGameModel.MoveCellDown();
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