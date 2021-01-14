using UnityEngine;

public class InGamePresenter : MonoBehaviour
{
    private InGameModel inGameModel;
    private InGameView inGameView;

    private void Start()
    {
        inGameModel = GetComponent<InGameModel>();
        inGameView = GetComponent<InGameView>();

        // Modelの値の変更を監視する
        inGameModel.ChangeScore += inGameView.SetScore;
        inGameModel.ApplyStage += inGameView.ApplyStage;
        inGameModel.GameOver += GameOver;

        // Viewの入力を監視する
        inGameView.InputRightKey += inGameModel.MoveCellRight;
        inGameView.InputLeftKey += inGameModel.MoveCellLeft;
        inGameView.InputUpKey += inGameModel.MoveCellUp;
        inGameView.InputDownKey += inGameModel.MoveCellDown;

        // ステージの初期状態を生成
        inGameModel.InitStage();

    }

    /// <summary>
    /// ゲームオーバー時に行う処理
    /// </summary>
    private void GameOver()
    {
        SaveScore(inGameModel.GetScore()); //引数を渡すのと何が違う？
        LoadResultScene();
    }

    /// <summary>
    /// スコアをセーブする
    /// </summary>
    /// <param name="score">スコア</param>
    private void SaveScore(int score)
    {
        ScoreManager.Instance.SaveScore(score);
    }

    /// <summary>
    /// リザルトシーンへ遷移する
    /// </summary>
    private void LoadResultScene()
    {
        SceneController.Instance.LoadScene(SceneNames.Result);
    }

}