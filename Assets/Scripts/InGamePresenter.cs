using UnityEngine;

public class InGamePresenter : MonoBehaviour
{
    private InGameModel inGameModel;
    private InGameView inGameView;
    private MenuWindowView menuWindowView;

    private void Start()
    {
        inGameModel = GetComponent<InGameModel>();
        inGameView = GetComponent<InGameView>();
        menuWindowView = GetComponent<MenuWindowView>();

        // Modelの値の変更を監視する
        inGameModel.ChangeScore += inGameView.SetScore;
        inGameModel.ApplyStage += inGameView.ApplyStage;
        inGameModel.GameOver += GameOver;

        // Viewの入力を監視する
        inGameView.InputRightKey += MoveCellRight;
        inGameView.InputLeftKey += MoveCellLeft;
        inGameView.InputUpKey += MoveCellUp;
        inGameView.InputDownKey += MoveCellDown;
        menuWindowView.OnClickRestartButton += RestartGame;

        // 初期化
        inGameModel.InitStage();
        inGameModel.SetHighScore(LoadScore());

    }

    private void MoveCellRight()
    {
        if (menuWindowView.IsActive()) { return; }
        inGameModel.MoveCellRight();
    }
    private void MoveCellLeft()
    {
        if (menuWindowView.IsActive()) { return; }
        inGameModel.MoveCellLeft();
    }
    private void MoveCellUp()
    {
        if (menuWindowView.IsActive()) { return; }
        inGameModel.MoveCellUp();
    }
    private void MoveCellDown()
    {
        if (menuWindowView.IsActive()) { return; }
        inGameModel.MoveCellDown();
    }

    /// <summary>
    /// ゲームオーバー時に行う処理
    /// </summary>
    private void GameOver()
    {
        //ハイスコアのセーブ
        int score = inGameModel.GetScore();
        int highScore = inGameModel.GetHighScore();
        if (score == highScore)
        {
            SaveScore(highScore);
        }

        LoadResultScene();
    }

    /// <summary>
    /// ゲームをリスタートする
    /// </summary>
    private void RestartGame()
    {
        inGameModel.InitStage();
        inGameModel.ResetScore();
        menuWindowView.CloseWindow();
    }

    /// <summary>
    /// スコアをセーブする
    /// </summary>
    /// <param name="score">スコア</param>
    private void SaveScore(int score)
    {
        ScoreManager.Instance.SaveScore(score);
    }

    private int LoadScore()
    {
        return ScoreManager.Instance.LoadScore();
    }

    /// <summary>
    /// リザルトシーンへ遷移する
    /// </summary>
    private void LoadResultScene()
    {
        SceneController.Instance.LoadScene(SceneNames.Result);
    }


}